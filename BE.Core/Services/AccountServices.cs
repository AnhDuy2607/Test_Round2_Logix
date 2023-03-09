using BE.Core.Interfaces;
using BE.DTOs.Input;
using BE.DTOs.Output;
using BE.DTOs.ResponseTemplate;
using BE.Model.Entities;
using BE.UnitOfWork.Interface;
using BE.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Core.Services
{
    public class AccountServices : IAccountServices
    {
        private readonly IUnitOfWork _uow;
        public AccountServices(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<ResponseData<LoginOutput>> Authentication(LoginInput loginInput)
        {
            var account = await _uow.GetRepository<Account>()
                .AsQueryable()
                .Include(x => x.User)
                .SingleOrDefaultAsync(x => (!loginInput.UsernameIsEmail ? x.Username.Equals(loginInput.Username) : x.User.Email.Equals(loginInput.Username)) && x.HashPwd.Equals(loginInput.Password) && x.isActive);

            if (account == null || account.User == null)
            {
                return new ResponseData<LoginOutput>
                {
                    Data = null,
                    Code = 404,
                    Message = "not found account"
                };
            }

            if (string.IsNullOrEmpty(account.Token) || account.ExpiredTime.GetValueOrDefault() <= DateTime.Now)
            {
                var info = await UpdateLoginInfo(account);
                return new ResponseData<LoginOutput>
                {
                    Data = new LoginOutput
                    {
                        Token = info.token,
                        ExpiredDate = info.expiredDate,
                        User = new UserOutput
                        {
                            Id = account.User.Id,
                            FirstName = account.User.Firstname,
                            LastName = account.User.Lastname
                        }
                    },
                    Code = 200
                };
            }

            return new ResponseData<LoginOutput>
            {
                Data = new LoginOutput
                {
                    Token = account.Token,
                    ExpiredDate = account.ExpiredTime.GetValueOrDefault(),
                    User = new UserOutput
                    {
                        Id = account.User.Id,
                        FirstName = account.User.Firstname,
                        LastName = account.User.Lastname
                    }
                },
                Code = 200
            };
        }

        private async Task<(string token, DateTime expiredDate)> UpdateLoginInfo(Account account)
        {
            if (account == null)
            {
                return (string.Empty, DateTime.Now);
            }

            account.Token = Guid.NewGuid().ToString();
            account.ExpiredTime = DateTime.Now.AddDays(7); ;

            _uow.GetRepository<Account>().Update(account);

            await _uow.SaveChangesAsync();
            return (account.Token, account.ExpiredTime.GetValueOrDefault());
        }

        public async Task<ResponseData<ValidateTokenOutput>> ValidateToken(string token)
        {
            var response = new ResponseData<ValidateTokenOutput>();
            if (string.IsNullOrEmpty(token))
            {
                response.Data = new ValidateTokenOutput
                {
                    isValid = false,
                    Username = String.Empty
                };
                return response;
            }
            var result = await _uow.GetRepository<Account>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Select(x => new
                    {
                        x.Token,
                        x.Username,
                        x.Id,
                        x.ExpiredTime,
                        x.UserId
                    })
                    .SingleOrDefaultAsync(x => x.Token.Equals(token) && x.ExpiredTime.HasValue && x.ExpiredTime.Value >= DateTime.Now);

            if (result == null)
            {
                response.Data = new ValidateTokenOutput
                {
                    isValid = false,
                    Username = String.Empty
                };
                return response;
            }

            response.Data = new ValidateTokenOutput
            {
                isValid = true,
                Username = result.Username,
                AccountId = result.Id,
                UserId = result.UserId.GetValueOrDefault()
            };
            return response;
        }

        public async Task Logout(Guid accountId)
        {
            var account = await _uow.GetRepository<Account>().GetByIdAsync(accountId);
            if (account != null)
            {
                account.Token = null;
                account.ExpiredTime = null;
                _uow.GetRepository<Account>().Update(account);
                await _uow.SaveChangesAsync();
            }
        }

        public async Task<ResponseData<AccountOuput>> NewAccount(NewAccountInput model)
        {
            _uow.BeginTransaction();
            var response = new ResponseData<AccountOuput>();
            try
            {
                var user = new User
                {
                    Lastname = model.LastName,
                    Firstname = model.FirstName,
                    Email = model.Email
                };

                var account = new Account
                {
                    UserId = user.Id,
                    Username = model.Username,
                    HashPwd = Helper.GeneratePasswordWithSalt(model.Password),
                    isActive = true
                };

                _uow.GetRepository<User>().Add(user);
                _uow.GetRepository<Account>().Add(account);

                var result = await _uow.SaveChangesAsync() != 0;

                if (result)
                {
                    _uow.CommitTransaction();
                    response.Data = new AccountOuput
                    {
                        Status = account.isActive ? ConstantValue.ACTIVE_STATUS : ConstantValue.INACTIVE_STATUS,
                        Email = user.Email,
                        Username = account.Username
                    };
                }
                else
                {
                    _uow.RollbackTransaction();
                    response.Data = null;
                }
            }
            catch
            {
                _uow.RollbackTransaction();
                response.Data = null;
            }
            return response;
        }
    }
}

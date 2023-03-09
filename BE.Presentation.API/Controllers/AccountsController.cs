using BE.Core.Interfaces;
using BE.DTOs.Input;
using BE.Presentation.API.Controllers.Base;
using BE.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE.Presentation.API.Controllers
{
    public class AccountsController : BaseController
    {
        private readonly IAccountServices _accountServices;
        public AccountsController(IAccountServices accountServices)
        {
            _accountServices = accountServices;
        }

        [HttpPost("newAccount")]
        public async Task<IActionResult> NewAccount(NewAccountInput newAccount)
        {
            if (newAccount == null)
            {
                return BadRequest();
            }

            if (!newAccount.isMatchedPassword)
            {
                return BadRequest();
            }

            var result = await _accountServices.NewAccount(newAccount);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate(LoginInput loginInfo)
        {
            if (loginInfo == null)
            {
                return BadRequest();
            }

            if (string.IsNullOrEmpty(loginInfo.Username) || string.IsNullOrEmpty(loginInfo.Password))
            {
                return BadRequest();
            }
            loginInfo.Password = Helper.GeneratePasswordWithSalt(loginInfo.Password);
            var res = await _accountServices.Authentication(loginInfo);
            return res.Code != 200 ? BadRequest(res) : Ok(res);
        }

        [HttpPut("logout"), Authorize]
        public async Task<IActionResult> Logout()
        {
            if (AccountId != null)
            {
                await _accountServices.Logout(new Guid(AccountId));
                return Ok();
            }
            return NoContent();
        }
    }
}

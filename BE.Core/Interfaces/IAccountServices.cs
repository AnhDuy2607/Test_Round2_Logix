using BE.DTOs.Input;
using BE.DTOs.Output;
using BE.DTOs.ResponseTemplate;

namespace BE.Core.Interfaces
{
    public interface IAccountServices
    {
        Task<ResponseData<ValidateTokenOutput>> ValidateToken(string token);
        Task<ResponseData<LoginOutput>> Authentication(LoginInput loginInput);
        Task<ResponseData<AccountOuput>> NewAccount(NewAccountInput model);
        Task Logout(Guid accountId);
    }
}

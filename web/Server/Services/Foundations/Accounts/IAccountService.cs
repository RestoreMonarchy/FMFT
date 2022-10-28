using FMFT.Extensions.Authentication.Models;
using FMFT.Web.Server.Models.Accounts;
using FMFT.Web.Server.Models.Accounts.Params;

namespace FMFT.Web.Server.Services.Foundations.Accounts
{
    public interface IAccountService
    {
        ValueTask AuthorizeAccountAsync();
        ValueTask AuthorizeAccountByUserIdAsync(int authorizedUserId);
        ValueTask<AccountToken> CreateTokenAsync(CreateTokenParams @params);
        ValueTask<Account> RetrieveAccountAsync();
    }
}
using FMFT.Web.Client.Models.Accounts;
using FMFT.Web.Client.Models.Accounts.Requests;
using FMFT.Web.Client.Models.AccountTokens;

namespace FMFT.Web.Client.Services.Foundations.Accounts
{
    public interface IAccountService
    {
        ValueTask<AccountToken> LoginAsync(LogInWithPasswordRequest request);
        ValueTask<AccountToken> RegisterAsync(RegisterWithPasswordRequest request);
        ValueTask<UserAccount> RetrieveAccountAsync();
    }
}

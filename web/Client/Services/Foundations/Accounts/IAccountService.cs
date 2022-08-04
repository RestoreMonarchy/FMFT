using FMFT.Web.Client.Models.Accounts;
using FMFT.Web.Client.Models.Accounts.Requests;

namespace FMFT.Web.Client.Services.Foundations.Accounts
{
    public interface IAccountService
    {
        ValueTask<Account> ConfirmExternalLoginAsync(ConfirmExternalLoginRequest request);
        ValueTask<Account> LoginAsync(LogInWithPasswordRequest request);
        ValueTask<Account> RegisterAsync(RegisterWithPasswordRequest request);
        ValueTask<Account> RetrieveAccountAsync();
    }
}

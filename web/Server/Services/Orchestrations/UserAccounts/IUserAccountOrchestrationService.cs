using FMFT.Web.Shared.Models.Accounts;
using FMFT.Web.Shared.Models.UserAccounts.Requests;
using FMFT.Web.Shared.Models.Users.Params;

namespace FMFT.Web.Server.Services.Orchestrations.UserAccounts
{
    public interface IUserAccountOrchestrationService
    {
        ValueTask ChallengeExternalLoginAsync(string provider, string returnUrl);
        ValueTask<Account> ConfirmExternalLoginAsync(ConfirmExternalLoginRequest request);
        ValueTask<Account> HandleExternalLoginCallbackAsync();
        ValueTask<Account> RegisterWithPasswordAsync(RegisterWithPasswordRequest request);
        Account RetrieveAccount();
        ValueTask<Account> SignInWithPasswordAsync(SignInWithPasswordRequest request);
        ValueTask SignOutAsync();
    }
}

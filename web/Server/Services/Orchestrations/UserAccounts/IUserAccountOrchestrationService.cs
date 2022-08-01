using FMFT.Web.Shared.Models.Accounts;
using FMFT.Web.Shared.Models.UserAccounts.Requests;

namespace FMFT.Web.Server.Services.Orchestrations.UserAccounts
{
    public interface IUserAccountOrchestrationService
    {
        ValueTask ChallengeExternalLoginAsync(string provider, string returnUrl);
        ValueTask<Account> HandleExternalLoginCallbackAsync();
        ValueTask<Account> RegisterWithPasswordAsync(RegisterWithPasswordRequest request);
        ValueTask<Account> SignInWithPasswordAsync(SignInWithPasswordRequest request);
    }
}

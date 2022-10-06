using FMFT.Extensions.Authentication.Models;
using FMFT.Web.Server.Models.Accounts;
using FMFT.Web.Server.Models.Accounts.Params;

namespace FMFT.Web.Server.Services.Foundations.Accounts
{
    public interface IAccountService
    {
        ValueTask ChallengeExternalLoginAsync(ChallengeExternalLoginParams @params);
        ValueTask<Account> RetrieveAccountAsync();
        ValueTask<ExternalLogin> RetrieveExternalLoginAsync();
        ValueTask SignInAccountAsync(SignInAccountParams @params);
        ValueTask SignOutAccountAsync();
    }
}
using FMFT.Extensions.Authentication.Models;
using FMFT.Web.Shared.Models.Accounts;
using FMFT.Web.Shared.Models.Accounts.Params;

namespace FMFT.Web.Server.Services.Foundations.Accounts
{
    public interface IAccountService
    {
        ValueTask ChallengeExternalLoginAsync(string provider, string redirectUrl);
        Account RetrieveAccount();
        ValueTask<ExternalLogin> RetrieveExternalLoginAsync();
        ValueTask SignInAccountAsync(SignInAccountParams @params);
        ValueTask SignOutAccountAsync();
    }
}
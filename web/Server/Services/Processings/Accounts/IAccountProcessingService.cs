using FMFT.Web.Server.Models.Accounts;
using FMFT.Web.Server.Models.Accounts.Arguments;
using FMFT.Web.Server.Models.Accounts.Params;
using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Server.Services.Processings.Accounts
{
    public interface IAccountProcessingService
    {
        void AuthorizeAccountByUserId(int authorizedUserId);
        void AuthorizeAccountByUserIdOrRoles(int authorizedUserId, params UserRole[] authorizedRoles);
        void AuthorizeAccountByRole(params UserRole[] authorizedRoles);
        ValueTask ChallengeExternalLoginAsync(ChallengeExternalLoginArguments arguments);
        Account RetrieveAccount();
        ValueTask<ExternalLogin> RetrieveExternalLoginAsync();
        ValueTask SignInAccountAsync(SignInAccountParams @params);
        ValueTask SignOutAccountAsync();
    }
}

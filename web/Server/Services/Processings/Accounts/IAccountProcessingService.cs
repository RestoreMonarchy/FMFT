using FMFT.Web.Server.Models.Accounts;
using FMFT.Web.Server.Models.Accounts.Arguments;
using FMFT.Web.Server.Models.Accounts.Params;
using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Server.Services.Processings.Accounts
{
    public interface IAccountProcessingService
    {
        ValueTask AuthorizeAccountByUserIdAsync(int authorizedUserId);
        ValueTask AuthorizeAccountByUserIdOrRolesAsync(int authorizedUserId, params UserRole[] authorizedRoles);
        ValueTask AuthorizeAccountByRoleAsync(params UserRole[] authorizedRoles);
        ValueTask ChallengeExternalLoginAsync(ChallengeExternalLoginArguments arguments);
        ValueTask<Account> RetrieveAccountAsync();
        ValueTask<ExternalLogin> RetrieveExternalLoginAsync();
        ValueTask SignInAccountAsync(SignInAccountParams @params);
        ValueTask SignOutAccountAsync();
        ValueTask AuthorizeAccountAsync();
    }
}

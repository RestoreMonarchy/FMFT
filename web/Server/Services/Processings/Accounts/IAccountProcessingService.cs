using FMFT.Web.Shared.Models.Accounts;
using FMFT.Web.Shared.Models.Accounts.Params;

namespace FMFT.Web.Server.Services.Processings.Accounts
{
    public interface IAccountProcessingService
    {
        Account RetrieveAccount();
        ValueTask<ExternalLogin> RetrieveExternalLoginAsync();
        ValueTask SignInAccountAsync(SignInAccountParams @params);
        ValueTask SignOutAccountAsync();
    }
}

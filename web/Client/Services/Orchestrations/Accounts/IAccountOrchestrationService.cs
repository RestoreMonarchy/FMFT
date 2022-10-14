using FMFT.Web.Client.Models.Accounts;
using FMFT.Web.Client.Models.Accounts.Requests;

namespace FMFT.Web.Client.Services.Orchestrations.Accounts
{
    public interface IAccountOrchestrationService
    {
        ValueTask LoginAsync(LogInWithPasswordRequest request);
        ValueTask RegisterAsync(RegisterWithPasswordRequest request);
        UserAccount RetrieveAccountStore();
        ValueTask UpdateAccountStoreAsync();
    }
}
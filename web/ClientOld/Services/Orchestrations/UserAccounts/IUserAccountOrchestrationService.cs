using FMFT.Web.Client.Models.Accounts;
using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Client.Services.Orchestrations.UserAccounts
{
    public interface IUserAccountOrchestrationService
    {
        UserAccount RetrieveAccountStore();
        ValueTask UpdateAccountCultureAsync(CultureId cultureId);
    }
}
using FMFT.Web.Client.Models.Accounts;
using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Client.Services.Orchestrations.UserAccounts
{
    public interface IUserAccountOrchestrationService
    {
        Account RetrieveAccount();
        ValueTask UpdateAccountCultureAsync(CultureId cultureId);
    }
}
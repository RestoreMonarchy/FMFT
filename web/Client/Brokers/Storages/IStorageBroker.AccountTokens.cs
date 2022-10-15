using FMFT.Web.Client.Models.API.Accounts;

namespace FMFT.Web.Client.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<AccountToken> GetAccountTokenAsync();
        ValueTask SetAccountTokenAsync(AccountToken accountToken);
    }
}

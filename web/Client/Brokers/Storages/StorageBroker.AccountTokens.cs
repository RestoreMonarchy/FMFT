using FMFT.Web.Client.Models.API.Accounts;

namespace FMFT.Web.Client.Brokers.Storages
{
    public partial class StorageBroker
    {
        private const string AccountTokenKey = "AccountToken";

        public async ValueTask<AccountToken> GetAccountTokenAsync()
        {
            return await GetLocalItemAsync<AccountToken>(AccountTokenKey);
        }

        public async ValueTask SetAccountTokenAsync(AccountToken accountToken)
        {
            await SetLocalItemAsync(CultureIdKey, accountToken);
        }
    }
}

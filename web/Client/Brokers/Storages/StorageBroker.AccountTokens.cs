namespace FMFT.Web.Client.Brokers.Storages
{
    public partial class StorageBroker
    {
        private const string AccountTokenKey = "AccountToken";

        public async ValueTask<string> GetAccountTokenAsync()
        {
            return await GetLocalItemAsync<string>(AccountTokenKey);
        }

        public async ValueTask SetAccountTokenAsync(string accountToken)
        {
            await SetLocalItemAsync(CultureIdKey, accountToken);
        }
    }
}

namespace FMFT.Web.Client.Brokers.Storages
{
    public partial class StorageBroker
    {
        public async ValueTask<bool> GetCookiesAlertFlagAsync()
        {
            return await GetLocalItemAsync<bool>("CookiesAlert");
        }

        public async ValueTask SetCookiesAlertFlagAsync(bool value)
        {
            await SetLocalItemAsync("CookiesAlert", value);
        }
    }
}

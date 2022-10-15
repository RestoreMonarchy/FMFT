using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Client.Brokers.Storages
{
    public partial class StorageBroker
    {
        private const string CultureIdKey = "CultureId";

        public async ValueTask<CultureId> GetCultureIdAsync()
        {
            return await GetLocalItemAsync<CultureId>(CultureIdKey);
        }

        public async ValueTask SetCultureIdAsync(CultureId cultureId)
        {
            await SetLocalItemAsync(CultureIdKey, cultureId);
        }
    }
}

using Blazored.LocalStorage;

namespace FMFT.Web.Client.Brokers.Storages
{
    public partial class StorageBroker : IStorageBroker
    {
        private readonly ILocalStorageService localStorage;

        public StorageBroker(ILocalStorageService localStorage)
        {
            this.localStorage = localStorage;
        }

        private async ValueTask SetItemAsync<T>(string key, T data)
        {
            await localStorage.SetItemAsync(key, data);
        }

        private async ValueTask<T> GetItemAsync<T>(string key)
        {
            return await localStorage.GetItemAsync<T>(key);
        }
    }
}

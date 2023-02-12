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

        private async ValueTask SetLocalItemAsync<T>(string key, T data)
        {
            await localStorage.SetItemAsync(key, data);
        }

        private async ValueTask<T> GetLocalItemAsync<T>(string key)
        {
            return await localStorage.GetItemAsync<T>(key);
        }

        private async ValueTask RemoveLocalItemAsync(string key)
        {
            await localStorage.RemoveItemAsync(key);
        }
    }
}

using FMFT.Web.Client.Models.Services.Orders;

namespace FMFT.Web.Client.Brokers.Storages
{
    public partial class StorageBroker
    {
        private const string OrderStateDataKey = "OrderStateData-{0}";

        public async ValueTask SetOrderStateDataAsync(int showId, OrderStateData orderState) 
        {
            string key = string.Format(OrderStateDataKey, showId);

            await SetLocalItemAsync(key, orderState);
        }

        public async ValueTask<OrderStateData> GetOrderStateDataAsync(int showId)
        {
            string key = string.Format(OrderStateDataKey, showId);

            return await GetLocalItemAsync<OrderStateData>(key);
        }

        public async ValueTask RemoveOrderStateDataAsync(int showId)
        {
            string key = string.Format(OrderStateDataKey, showId);

            await RemoveLocalItemAsync(key);
        }
    }
}

using FMFT.Web.Client.Models.Services.Orders;

namespace FMFT.Web.Client.Brokers.Storages
{
    public partial class StorageBroker
    {
        private const string OrderStateKey = "OrderState-{0}";

        public async ValueTask SetOrderStateAsync(int showId, OrderState orderState) 
        {
            string key = string.Format(OrderStateKey, showId);

            await SetLocalItemAsync(key, orderState);
        }

        public async ValueTask<OrderState> GetOrderStateAsync(int showId)
        {
            string key = string.Format(OrderStateKey, showId);

            return await GetLocalItemAsync<OrderState>(key);
        }
    }
}

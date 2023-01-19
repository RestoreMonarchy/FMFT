using FMFT.Web.Client.Models.Services.Orders;

namespace FMFT.Web.Client.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask SetOrderStateAsync(int showId, OrderState orderState);
        ValueTask<OrderState> GetOrderStateAsync(int showId);
    }
}

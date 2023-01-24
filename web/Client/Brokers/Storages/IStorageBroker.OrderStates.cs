using FMFT.Web.Client.Models.Services.Orders;

namespace FMFT.Web.Client.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask SetOrderStateDataAsync(int showId, OrderStateData orderStateData);
        ValueTask<OrderStateData> GetOrderStateDataAsync(int showId);
    }
}

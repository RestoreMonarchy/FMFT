using FMFT.Web.Server.Models.Orders;

namespace FMFT.Web.Server.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Order> SelectOrderByIdAsync(int orderId);
    }
}
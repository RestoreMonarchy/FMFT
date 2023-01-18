using FMFT.Web.Server.Models.Database;
using FMFT.Web.Server.Models.Orders;
using FMFT.Web.Server.Models.Orders.DTOs;

namespace FMFT.Web.Server.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Order> SelectOrderByIdAsync(int orderId);
        ValueTask<StoredProcedureResult<Order>> CreateOrderAsync(CreateOrderDTO dto);
    }
}
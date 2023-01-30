using FMFT.Web.Server.Models.Database;
using FMFT.Web.Server.Models.Orders;
using FMFT.Web.Server.Models.Orders.DTOs;
using FMFT.Web.Server.Models.Orders.Params;

namespace FMFT.Web.Server.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<IEnumerable<Order>> SelectAllOrdersAsync();
        ValueTask<Order> SelectOrderByIdAsync(int orderId);
        ValueTask<IEnumerable<Order>> SelectOrdersByUserIdAsync(int userId);
        ValueTask<StoredProcedureResult<Order>> CreateOrderAsync(CreateOrderDTO dto);
        ValueTask<StoredProcedureResult<Order>> UpdateOrderPaymentTokenAsync(UpdateOrderPaymentTokenParams @params);
    }
}
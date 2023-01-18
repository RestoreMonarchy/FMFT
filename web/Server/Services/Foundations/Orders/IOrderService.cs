using FMFT.Web.Server.Models.Orders;
using FMFT.Web.Server.Models.Orders.Params;

namespace FMFT.Web.Server.Services.Foundations.Orders
{
    public interface IOrderService
    {
        ValueTask<Order> CreateOrderAsync(CreateOrderParams @params);
        ValueTask<Order> RetrieveOrderByIdAsync(int orderId);
    }
}
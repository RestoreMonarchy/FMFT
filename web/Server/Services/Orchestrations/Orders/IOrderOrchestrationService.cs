using FMFT.Web.Server.Models.Orders;
using FMFT.Web.Server.Models.Orders.Params;

namespace FMFT.Web.Server.Services.Orchestrations.Orders
{
    public interface IOrderOrchestrationService
    {
        ValueTask<Order> CreateOrderAsync(CreateOrderParams @params);
        ValueTask<Order> RetrieveOrderByIdAsync(int orderId);
    }
}
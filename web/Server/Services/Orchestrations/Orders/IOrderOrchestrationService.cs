using FMFT.Web.Server.Models.Orders;

namespace FMFT.Web.Server.Services.Orchestrations.Orders
{
    public interface IOrderOrchestrationService
    {
        ValueTask<Order> RetrieveOrderByIdAsync(int orderId);
    }
}
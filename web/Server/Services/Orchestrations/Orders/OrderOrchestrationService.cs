using FMFT.Web.Server.Brokers.Loggings;
using FMFT.Web.Server.Models.Orders;
using FMFT.Web.Server.Services.Foundations.Emails;
using FMFT.Web.Server.Services.Foundations.Orders;

namespace FMFT.Web.Server.Services.Orchestrations.Orders
{
    public class OrderOrchestrationService : IOrderOrchestrationService
    {
        private readonly ILoggingBroker loggingBroker;
        private readonly IOrderService orderService;

        public OrderOrchestrationService(ILoggingBroker loggingBroker, IOrderService orderService)
        {
            this.loggingBroker = loggingBroker;
            this.orderService = orderService;
        }

        public async ValueTask<Order> RetrieveOrderByIdAsync(int orderId)
        {
            Order order = await orderService.RetrieveOrderByIdAsync(orderId);

            return order;
        }
    }
}

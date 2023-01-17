using FMFT.Web.Server.Brokers.Loggings;
using FMFT.Web.Server.Brokers.Storages;
using FMFT.Web.Server.Models.Orders;
using FMFT.Web.Server.Models.Orders.Exceptions;

namespace FMFT.Web.Server.Services.Foundations.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public OrderService(ILoggingBroker loggingBroker, IStorageBroker storageBroker)
        {
            this.loggingBroker = loggingBroker;
            this.storageBroker = storageBroker;
        }

        public async ValueTask<Order> RetrieveOrderByIdAsync(int orderId)
        {
            Order order = await storageBroker.SelectOrderByIdAsync(orderId);

            if (order == null)
            {
                throw new NotFoundOrderException();
            }

            return order;
        }
    }
}

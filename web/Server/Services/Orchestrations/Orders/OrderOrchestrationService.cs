using FMFT.Web.Server.Brokers.Loggings;
using FMFT.Web.Server.Models.Orders;
using FMFT.Web.Server.Models.Orders.Exceptions;
using FMFT.Web.Server.Models.Orders.Params;
using FMFT.Web.Server.Models.Reservations.Exceptions;
using FMFT.Web.Server.Services.Foundations.Orders;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        public async ValueTask<IEnumerable<Order>> RetrieveAllOrdersAsync()
        {
            IEnumerable<Order> orders = await orderService.RetrieveAllOrdersAsync();

            return orders;
        }

        public async ValueTask<Order> RetrieveOrderByIdAsync(int orderId)
        {
            Order order = await orderService.RetrieveOrderByIdAsync(orderId);

            return order;
        }

        public async ValueTask<Order> RetrieveOrderBySessionIdAsync(Guid sessionId)
        {
            return await orderService.RetrieveOrderBySessionIdAsync(sessionId);
        }

        public async ValueTask<IEnumerable<Order>> RetrieveOrdersByUserIdAsync(int userId)
        {
            IEnumerable<Order> orders = await orderService.RetrieveOrdersByUserIdAsync(userId);

            return orders;
        }

        public async ValueTask<Order> CreateOrderAsync(CreateOrderParams @params)
        {
            CreateUserOrderReservationValidationException validationException = new();
            const int maximumItemsQuantity = 20;
            const int minimumItemsQuantity = 1;

            int quantity = @params.Items.Sum(x => x.Quantity);
            if (quantity > maximumItemsQuantity || quantity < minimumItemsQuantity)
            {
                validationException.UpsertDataList("Items", $"The amount of items that can be reserved in one order must be in range between {minimumItemsQuantity} and {maximumItemsQuantity}");
            }

            Order order = await orderService.CreateOrderAsync(@params);

            return order;
        }

        public async ValueTask<Order> UpdateOrderPaymentTokenAsync(UpdateOrderPaymentTokenParams @params)
        {
            return await orderService.UpdateOrderPaymentTokenAsync(@params);
        }

        public async ValueTask<Order> UpdateOrderStatusAsync(UpdateOrderStatusParams @params)
        {
            return await orderService.UpdateOrderStatusAsync(@params);
        }
    }
}

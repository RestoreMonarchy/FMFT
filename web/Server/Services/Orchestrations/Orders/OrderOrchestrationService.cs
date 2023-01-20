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

        public async ValueTask<IEnumerable<Order>> RetrieveOrdersByUserIdAsync(int userId)
        {
            IEnumerable<Order> orders = await orderService.RetrieveOrdersByUserIdAsync(userId);

            return orders;
        }

        public async ValueTask<Order> CreateOrderAsync(CreateOrderParams @params)
        {
            CreateUserOrderReservationValidationException validationException = new();
            const int maximumSeats = 100;
            const int minimumSeats = 1;

            if (@params.SeatIds.Count > maximumSeats || @params.SeatIds.Count < minimumSeats)
            {
                validationException.UpsertDataList("SeatIds", $"The amount of seats that can be reserved in one order must be in range between {minimumSeats} and {maximumSeats}");
            }

            Order order = await orderService.CreateOrderAsync(@params);

            return order;
        }


    }
}

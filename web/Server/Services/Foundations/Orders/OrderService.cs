using FMFT.Web.Server.Brokers.Loggings;
using FMFT.Web.Server.Brokers.Storages;
using FMFT.Web.Server.Models.Database;
using FMFT.Web.Server.Models.Orders;
using FMFT.Web.Server.Models.Orders.DTOs;
using FMFT.Web.Server.Models.Orders.Exceptions;
using FMFT.Web.Server.Models.Orders.Params;
using FMFT.Web.Server.Models.Reservations.DTOs;
using FMFT.Web.Server.Models.Reservations.Exceptions;
using FMFT.Web.Server.Models.Reservations.Params;

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

        public async ValueTask<Order> CreateOrderAsync(CreateOrderParams @params)
        {

            List<CreateOrderItemDTO> orderItems = new();

            @params.Items.ForEach (item => orderItems.Add(
                new()
                {
                    ShowProductId = item.ShowProductId,
                    Price = item.Price,
                    Quantity= item.Quantity
                })
            );

            CreateOrderDTO dto = new()
            {
                UserId = @params.UserId,
                Amount = @params.Amount,
                Currency = @params.Currency,
                PaymentMethod = @params.PaymentMethod,
                ExpireDate = @params.ExpireDate,
                Items = orderItems,
                SeatIds = @params.SeatIds,

            };

            StoredProcedureResult<Order> result = await storageBroker.CreateOrderAsync(dto);

            if (result.ReturnValue == 1)
            {
                throw new SeatAlreadyReservedReservationException();
            }

            if (result.ReturnValue == 2)
            {
                throw new UserAlreadyReservedReservationException();
            }

            if (result.ReturnValue == 3)
            {
                throw new SeatsNotProvidedReservationException();
            }

            return result.Result;
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

using FMFT.Web.Server.Brokers.Loggings;
using FMFT.Web.Server.Brokers.Storages;
using FMFT.Web.Server.Models.Database;
using FMFT.Web.Server.Models.Orders;
using FMFT.Web.Server.Models.Orders.DTOs;
using FMFT.Web.Server.Models.Orders.Exceptions;
using FMFT.Web.Server.Models.Orders.Params;

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

        public async ValueTask<IEnumerable<Order>> RetrieveAllOrdersAsync()
        {
            return await storageBroker.SelectAllOrdersAsync();
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

        public async ValueTask<Order> RetrieveOrderBySessionIdAsync(Guid sessionId)
        {
            Order order = await storageBroker.SelectOrderBySessionIdAsync(sessionId);

            if (order == null)
            {
                throw new NotFoundOrderException();
            }

            return order;

        }

        public async ValueTask<IEnumerable<Order>> RetrieveOrdersByUserIdAsync(int userId)
        {
            return await storageBroker.SelectOrdersByUserIdAsync(userId);
        }

        public async ValueTask<Order> UpdateOrderPaymentTokenAsync(UpdateOrderPaymentTokenParams @params)
        {
            StoredProcedureResult<Order> result = await storageBroker.UpdateOrderPaymentTokenAsync(@params);

            if (result.ReturnValue == 1)
            {
                throw new NotFoundOrderException();
            }

            return result.Result;
        }

        public async ValueTask<Order> UpdateOrderStatusAsync(UpdateOrderStatusParams @params)
        {
            StoredProcedureResult<Order> result = await storageBroker.UpdateOrderStatusAsync(@params);

            if (result.ReturnValue == 1)
            {
                throw new NotFoundOrderException();
            }

            return result.Result;
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
                ExpireDate = DateTime.Now.AddMinutes(15),
                Items = orderItems,
                SeatIds = @params.SeatIds,

            };

            StoredProcedureResult<Order> result = await storageBroker.CreateOrderAsync(dto);

            if (result.ReturnValue == 1)
            {
                throw new SeatAlreadyReservedOrderReservationException();
            }

            if (result.ReturnValue == 2)
            {
                throw new UserAlreadyReservedOrderReservationException();
            }

            if (result.ReturnValue == 3)
            {
                throw new SeatsNotProvidedOrderReservationException();
            }

            if (result.ReturnValue == 101)
            {
                throw new NotMatchOrderedItemsQtyWithSeatsOrderException();
            }

            if (result.ReturnValue == 102)
            {
                throw new OrderedQtyTooLargeOrderException();
            }

            if (result.ReturnValue == 103)
            {
                throw new InvalidShowProductIdOrderException();
            }

            if (result.ReturnValue == 104)
            {
                throw new OrderAmountInvalidException();
            }

            if (result.ReturnValue == 105)
            {
                throw new OrderAmountMismatchException();
            }

            return result.Result;
        }

    }
}

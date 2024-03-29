﻿using FMFT.Web.Server.Brokers.Loggings;
using FMFT.Web.Server.Brokers.Storages;
using FMFT.Web.Server.Models.Database;
using FMFT.Web.Server.Models.Orders;
using FMFT.Web.Server.Models.Orders.DTOs;
using FMFT.Web.Server.Models.Orders.Exceptions;
using FMFT.Web.Server.Models.Orders.Params;
using FMFT.Web.Server.Models.Reservations.Exceptions;

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
                    Quantity= item.Quantity,
                    SeatIds = item.SeatIds ?? new List<int>()
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
                PaymentProvider = @params.PaymentProvider
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

            if (result.ReturnValue == 4)
            {
                throw new DuplicateSeatsReservationException();
            }

            if (result.ReturnValue == 5)
            {
                throw new SeatsNotProvidedOrderReservationException();
            }

            if (result.ReturnValue == 6)
            {
                throw new InvalidShowReservationException();
            }

            if (result.ReturnValue == 7)
            {
                throw new ShowProductOverbookedReservationException();
            }

            if (result.ReturnValue == 6)
            {
                throw new SeatsInvalidReservationException();
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

            if (result.ReturnValue == 106)
            {
                throw new FromThePastShowOrderException();
            }

            if (result.ReturnValue == 110)
            {
                throw new ShowDisabledOrderException();
            }

            if (result.ReturnValue == 111)
            {
                throw new ShowSellNotStartedOrderException();
            }

            return result.Result;
        }
    }
}

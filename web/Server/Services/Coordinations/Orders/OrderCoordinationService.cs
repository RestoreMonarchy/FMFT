using FMFT.Extensions.Payments.Models.Enums;
using FMFT.Web.Server.Brokers.Loggings;
using FMFT.Web.Server.Models.Orders;
using FMFT.Web.Server.Models.Orders.Params;
using FMFT.Web.Server.Models.Payments;
using FMFT.Web.Server.Models.Payments.Params;
using FMFT.Web.Server.Models.Reservations;
using FMFT.Web.Server.Models.Reservations.Params;
using FMFT.Web.Server.Models.UserAccounts;
using FMFT.Web.Server.Services.Orchestrations.Orders;
using FMFT.Web.Server.Services.Orchestrations.Payments;
using FMFT.Web.Server.Services.Orchestrations.Reservations;
using FMFT.Web.Server.Services.Orchestrations.UserAccounts;
using FMFT.Web.Shared.Enums;
using RESTFulSense.Exceptions;
using System.Data;

namespace FMFT.Web.Server.Services.Coordinations.Orders
{
    public partial class OrderCoordinationService : IOrderCoordinationService
    {
        private readonly ILoggingBroker loggingBroker;
        private readonly IOrderOrchestrationService orderService;
        private readonly IReservationOrchestrationService reservationService;
        private readonly IUserAccountOrchestrationService userAccountService;
        private readonly IPaymentOrchestrationService paymentService;

        public OrderCoordinationService(ILoggingBroker loggingBroker,
            IOrderOrchestrationService orderService,
            IReservationOrchestrationService reservationService,
            IUserAccountOrchestrationService userAccountService,
            IPaymentOrchestrationService paymentService)
        {
            this.loggingBroker = loggingBroker;
            this.orderService = orderService;
            this.reservationService = reservationService;
            this.userAccountService = userAccountService;
            this.paymentService = paymentService;
        }

        public async ValueTask<IEnumerable<Order>> RetrieveAllOrdersAsync()
        {
            await userAccountService.AuthorizeUserAccountByRoleAsync(UserRole.Admin);

            return await orderService.RetrieveAllOrdersAsync();
        }

        public async ValueTask<Order> RetrieveOrderByIdAsync(int orderId)
        {
            Order order = await orderService.RetrieveOrderByIdAsync(orderId);

            await userAccountService.AuthorizeUserAccountByUserIdOrRolesAsync(order.UserId(), UserRole.Admin);

            return order;
        }

        public async ValueTask<IEnumerable<Order>> RetrieveOrdersForCurrentUserAsync()
        {
            UserAccount user = await userAccountService.RetrieveUserAccountAsync();

            return await orderService.RetrieveOrdersByUserIdAsync(user.UserId);
        }

        public async ValueTask<IEnumerable<Order>> RetrieveOrdersByUserIdAsync(int userId)
        {
            await userAccountService.AuthorizeUserAccountByUserIdOrRolesAsync(userId, UserRole.Admin);

            return await orderService.RetrieveOrdersByUserIdAsync(userId);
        }

        public async ValueTask<Order> RetrieveOrderBySessionIdAsync(Guid sessionId)
        {
            Order order = await orderService.RetrieveOrderBySessionIdAsync(sessionId);

            await userAccountService.AuthorizeUserAccountByUserIdOrRolesAsync(order.UserId(), UserRole.Admin);

            return order;
        }

        public async ValueTask<IEnumerable<Reservation>> RetrieveReservationsByOrderIdAsync(int orderId)
        {
            Order order = await RetrieveOrderByIdAsync(orderId);
            int userId = order.UserId();
            await userAccountService.AuthorizeUserAccountByUserIdOrRolesAsync(userId, UserRole.Admin);

            return await reservationService.RetrieveReservationsByOrderIdAsync(orderId);
        }

        public async ValueTask<Order> CreateOrderAsync(CreateOrderParams @params)
        {
            await userAccountService.AuthorizeAccountAsync();

            @params.PaymentProvider = paymentService.GetPaymentProviderFromPaymentMethod(@params.PaymentMethod);

            Order order = await orderService.CreateOrderAsync(@params);

            await EnqueueExecuteOrderExpireAsync(order.Id, order.ExpireDate);

            RegisterPaymentParams @registerPaymentParams = MapOrderToRegisterPaymentParams(order);

            RegisteredPayment registeredPayment = await paymentService.RegisterPaymentAsync(registerPaymentParams);

            UpdateOrderPaymentTokenParams @updateOrderPaymentTokenParams = new()
            {
                OrderId = order.Id,
                PaymentToken = registeredPayment.Token
            };

            order = await orderService.UpdateOrderPaymentTokenAsync(@updateOrderPaymentTokenParams);            

            return order;
        }

        public async ValueTask ProcessPaymentNotificationAsync(PaymentProvider paymentProvider)
        {
            ProcessedPayment payment = await paymentService.ProcessPaymentNotificationAsync(paymentProvider);

            if (payment == null)
            {
                return;
            }

            Guid sessionId = payment.SessionId;
            Order order = await orderService.RetrieveOrderBySessionIdAsync(sessionId);

            UpdateOrderStatusParams @updateOrderStatusParams = new()
            {
                OrderId = order.Id,
                Status = OrderStatus.PaymentReceived
            };

            order = await orderService.UpdateOrderStatusAsync(@updateOrderStatusParams);

            IEnumerable<Reservation> reservations = await reservationService.RetrieveReservationsByOrderIdAsync(order.Id);

            foreach (Reservation reservation in reservations)
            {
                UpdateReservationStatusParams @updateReservationStatusParams = new()
                {
                    ReservationId = reservation.Id,
                    ReservationStatus = ReservationStatus.Ok
                };

                Reservation updatedReservation = await reservationService.UpdateReservationStatusAsync(updateReservationStatusParams);
                await reservationService.SendReservationSummaryEmailAsync(updatedReservation.User.Email, updatedReservation);
            }

            updateOrderStatusParams = new()
            {
                OrderId = order.Id,
                Status = OrderStatus.Completed
            };

            order = await orderService.UpdateOrderStatusAsync(updateOrderStatusParams);
            // Send order summary email
        }

        public async ValueTask<PaymentUrl> GetOrderPaymentUrlAsync(int orderId)
        {
            Order order = await orderService.RetrieveOrderByIdAsync(orderId);

            await userAccountService.AuthorizeAccountByUserIdAsync(order.User.Id);

            GetPaymentUrlParams @params = MapOrderToGetPaymentUrlParams(order);
            
            return await paymentService.GetPaymentUrlAsync(@params);
        }
    }
}

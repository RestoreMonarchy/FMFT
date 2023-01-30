using FMFT.Web.Server.Brokers.Loggings;
using FMFT.Web.Server.Models.Orders;
using FMFT.Web.Server.Models.Orders.Params;
using FMFT.Web.Server.Models.Payments;
using FMFT.Web.Server.Models.Payments.Params;
using FMFT.Web.Server.Models.UserAccounts;
using FMFT.Web.Server.Services.Orchestrations.Orders;
using FMFT.Web.Server.Services.Orchestrations.Payments;
using FMFT.Web.Server.Services.Orchestrations.Reservations;
using FMFT.Web.Server.Services.Orchestrations.UserAccounts;
using FMFT.Web.Shared.Enums;

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

        public async ValueTask<Order> CreateOrderAsync(CreateOrderParams @params)
        {
            await userAccountService.AuthorizeAccountAsync();

            Order order = await orderService.CreateOrderAsync(@params);
            await reservationService.SendReservationSummaryEmailForOrderAsync(order);

            RegisterPaymentParams @registerPaymentParams = MapOrderToRegisterPaymentParams(order);

            RegisteredPayment registeredPayment = await paymentService.RegisterPaymentAsync(registerPaymentParams);

            UpdateOrderPaymentTokenParams @updateOrderPaymentTokenParams = new()
            {
                OrderId = order.Id,
                PaymentToken = registeredPayment.Token
            };

            order = await orderService.UpdateOrderPaymentTokenAsync(updateOrderPaymentTokenParams);           

            return order;
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

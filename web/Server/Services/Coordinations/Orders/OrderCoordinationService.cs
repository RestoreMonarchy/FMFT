using FMFT.Web.Server.Brokers.Loggings;
using FMFT.Web.Server.Models.Orders;
using FMFT.Web.Server.Models.Orders.Params;
using FMFT.Web.Server.Models.UserAccounts;
using FMFT.Web.Server.Services.Orchestrations.Orders;
using FMFT.Web.Server.Services.Orchestrations.Reservations;
using FMFT.Web.Server.Services.Orchestrations.UserAccounts;
using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Server.Services.Coordinations.Orders
{
    public class OrderCoordinationService : IOrderCoordinationService
    {
        private readonly ILoggingBroker loggingBroker;
        private readonly IOrderOrchestrationService orderService;
        private readonly IReservationOrchestrationService reservationService;
        private readonly IUserAccountOrchestrationService userAccountService;

        public OrderCoordinationService(ILoggingBroker loggingBroker,
            IOrderOrchestrationService orderService,
            IReservationOrchestrationService reservationService,
            IUserAccountOrchestrationService userAccountService)
        {
            this.loggingBroker = loggingBroker;
            this.orderService = orderService;
            this.reservationService = reservationService;
            this.userAccountService = userAccountService;
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
            Order order = await orderService.CreateOrderAsync(@params);

            

            await reservationService.SendReservationSummaryEmailForOrderAsync(order);

            return order;
        }

    }
}

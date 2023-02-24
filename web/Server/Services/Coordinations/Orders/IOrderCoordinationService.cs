using FMFT.Web.Server.Models.Orders;
using FMFT.Web.Server.Models.Orders.Params;
using FMFT.Web.Server.Models.Payments;
using FMFT.Web.Server.Models.Reservations;
using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Server.Services.Coordinations.Orders
{
    public interface IOrderCoordinationService
    {
        ValueTask<Order> CreateOrderAsync(CreateOrderParams @params);
        ValueTask<PaymentUrl> GetOrderPaymentUrlAsync(int orderId);
        ValueTask ProcessPaymentNotificationAsync(PaymentProvider paymentProvider);
        ValueTask<IEnumerable<Order>> RetrieveAllOrdersAsync();
        ValueTask<Order> RetrieveOrderByIdAsync(int orderId);
        ValueTask<Order> RetrieveOrderBySessionIdAsync(Guid sessionId);
        ValueTask<IEnumerable<Order>> RetrieveOrdersByUserIdAsync(int userId);
        ValueTask<IEnumerable<Order>> RetrieveOrdersForCurrentUserAsync();
        ValueTask<IEnumerable<Reservation>> RetrieveReservationsByOrderIdAsync(int orderId);
    }
}
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Orders;
using FMFT.Web.Client.Models.API.Orders.Requests;
using FMFT.Web.Client.Models.API.Reservations;

namespace FMFT.Web.Client.Brokers.APIs
{
    public partial class APIBroker
    {
        private const string OrdersRelativeUrl = "api/orders";
        public async ValueTask<APIResponse<List<Reservation>>> GetOrderReservationsByIdAsync(int orderId)
        {
            return await GetAsync<List<Reservation>>($"{OrdersRelativeUrl}/{orderId}/reservations");
        }

        public async ValueTask<APIResponse<Order>> GetOrderByIdAsync(int orderId)
        {
            return await GetAsync<Order>($"{OrdersRelativeUrl}/{orderId}");
        }

        public async ValueTask<APIResponse<Order>> GetOrderBySessionIdAsync(Guid sessionId)
        {
            return await GetAsync<Order>($"{OrdersRelativeUrl}/session/{sessionId}");
        }

        public async ValueTask<APIResponse<Order>> CreateOrderAsync(CreateOrderRequest request)
        {
            string url = $"{OrdersRelativeUrl}/create";

            return await PostAsync<Order>(url, request);
        }

        public async ValueTask<APIResponse<PaymentUrl>> GetOrderPaymentUrlAsync(int orderId)
        {
            string url = $"{OrdersRelativeUrl}/{orderId}/pay";

            return await GetAsync<PaymentUrl>(url);
        }

        public async ValueTask<APIResponse<List<Order>>> GetOrdersByUserIdAsync(int userId)
        {
            string url = $"api/users/{userId}/orders";

            return await GetAsync<List<Order>>(url);
        }
    }
}

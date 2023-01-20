using FMFT.Web.Client.Models.API.Orders.Requests;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Reservations;
using FMFT.Web.Client.Models.API.Orders;

namespace FMFT.Web.Client.Brokers.APIs
{
    public partial interface IAPIBroker
    {
        ValueTask<APIResponse<List<Reservation>>> GetOrderReservationsByIdAsync(int orderId);
        ValueTask<APIResponse<Order>> GetOrderByIdAsync(int orderId);
        ValueTask<APIResponse<Order>> CreateOrderAsync(CreateOrderRequest request);

    }
}

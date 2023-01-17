using FMFT.Web.Server.Models.Orders;

namespace FMFT.Web.Server.Services.Coordinations.Orders
{
    public interface IOrderCoordinationService
    {
        ValueTask<Order> RetrieveOrderByIdAsync(int orderId);
    }
}
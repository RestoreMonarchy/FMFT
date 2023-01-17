using FMFT.Web.Server.Models.Orders;

namespace FMFT.Web.Server.Services.Foundations.Orders
{
    public interface IOrderService
    {
        ValueTask<Order> RetrieveOrderByIdAsync(int orderId);
    }
}
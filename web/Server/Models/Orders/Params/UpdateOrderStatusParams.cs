using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Server.Models.Orders.Params
{
    public class UpdateOrderStatusParams
    {
        public int OrderId { get; set; }
        public OrderStatus Status { get; set; }
    }
}

using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Client.Models.Services.Orders
{
    public class OrderStateData
    {
        public PaymentMethod PaymentMethod { get; set; }
        public List<int> SeatIds { get; set; }
        public List<OrderStateItemData> Items { get; set; }
    }
}

using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Client.Models.Services.Orders
{
    public class OrderStateData
    {
        public int ShowId { get; set; }
        public string CurrentStepKey { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public List<int> SeatIds { get; set; }
        public List<OrderItemStateData> Items { get; set; }
    }
}

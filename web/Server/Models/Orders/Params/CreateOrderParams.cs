using FMFT.Web.Server.Models.Orders.DTOs;

namespace FMFT.Web.Server.Models.Orders.Params
{
    public class CreateOrderParams
    {
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime ExpireDate { get; set; }
        public List<CreateOrderItemParams> Items { get; set; }
        public List<int> SeatIds { get; set; }
    }
}

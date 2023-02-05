using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Server.Models.Orders.Params
{
    public class CreateOrderParams
    {
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public PaymentProvider PaymentProvider { get; set; }
        public List<Item> Items { get; set; }
        public List<int> SeatIds { get; set; }

        public class Item
        {
            public int ShowProductId { get; set; }
            public decimal Price { get; set; }
            public int Quantity { get; set; }
        }
    }
}

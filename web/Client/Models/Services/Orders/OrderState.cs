using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Client.Models.Services.Orders
{
    public class OrderState
    {
        public OrderState()
        {
            Items = new();
            SeatIds = new();
        }
        
        public PaymentMethod PaymentMethod { get; set; }
        public List<OrderItemState> Items { get; set; }
        public List<int> SeatIds { get; set; }

        public decimal TotalPrice => Items.Sum(x => x.Quantity * x.ShowProduct.Price);
        public int TotalQuantity => Items.Sum(x => x.Quantity);
    }
}

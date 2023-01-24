using FMFT.Web.Client.Models.API.Seats;
using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Client.Models.Services.Orders
{
    public class OrderState
    {
        public OrderState()
        {
            Items = new();
            Seats = new();
        }
        
        public PaymentMethod PaymentMethod { get; set; }
        public List<OrderItemState> Items { get; set; }
        public List<Seat> Seats { get; set; }

        public decimal TotalPrice => Items.Sum(x => x.Quantity * x.ShowProduct.Price);
        public int TotalQuantity => Items.Sum(x => x.Quantity);

        public OrderStateData ToOrderStateData()
        {
            return new OrderStateData()
            {
                PaymentMethod = PaymentMethod,
                Items = Items.Select(x => new OrderStateItemData() 
                { 
                    ShowId = x.Show.Id,
                    ShowProductId = x.ShowProduct.Id,
                    Quantity = x.Quantity
                }).ToList(),
                SeatIds = Seats.Select(x => x.Id).ToList()
            };
        }
    }
}

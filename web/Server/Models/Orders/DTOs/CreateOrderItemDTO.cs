namespace FMFT.Web.Server.Models.Orders.DTOs
{
    public class CreateOrderItemDTO
    {
        public int ShowProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public List<int> SeatIds { get; set; }
    }
}

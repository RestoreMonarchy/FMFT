namespace FMFT.Web.Server.Models.Orders
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ShowProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}

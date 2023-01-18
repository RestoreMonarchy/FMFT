namespace FMFT.Web.Server.Models.Orders.Params
{
    public class CreateOrderItemParams
    {
        public int ShowProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}

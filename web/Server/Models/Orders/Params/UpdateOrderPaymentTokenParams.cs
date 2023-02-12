namespace FMFT.Web.Server.Models.Orders.Params
{
    public class UpdateOrderPaymentTokenParams
    {
        public int OrderId { get; set; }
        public string PaymentToken { get; set; }
    }
}

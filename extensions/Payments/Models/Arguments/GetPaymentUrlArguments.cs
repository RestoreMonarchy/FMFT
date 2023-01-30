namespace FMFT.Extensions.Payments.Models.Arguments
{
    public class GetPaymentUrlArguments
    {
        public string SessionId { get; set; }
        public string PaymentToken { get; set; }
    }
}

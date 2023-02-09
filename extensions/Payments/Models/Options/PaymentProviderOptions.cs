namespace FMFT.Extensions.Payments.Models.Options
{
    public class PaymentProviderOptions
    {
        public string NotifyUrl { get; set; }
        public string ReturnUrl { get; set; }
        public string MockPaymentUrl { get; set; }

        public string GetNotifyUrl(string paymentProvider)
        {
            return NotifyUrl.Replace("{paymentProviderId}", paymentProvider);
        }

        public string GetReturnUrl(int orderId)
        {
            return ReturnUrl.Replace("{orderId}", orderId.ToString());
        }

        public string GetMockPaymentUrl(string sessionId)
        {
            return MockPaymentUrl.Replace("sessionId", sessionId);
        }
    }
}

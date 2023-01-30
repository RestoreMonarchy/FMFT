using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Server.Models.Payments.Params
{
    public class GetPaymentUrlParams
    {
        public PaymentMethod PaymentMethod { get; set; }
        public string SessionId { get; set; }
        public string PaymentToken { get; set; }
    }
}

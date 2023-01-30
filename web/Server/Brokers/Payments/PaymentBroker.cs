using FMFT.Extensions.Payments.Services;

namespace FMFT.Web.Server.Brokers.Payments
{
    public class PaymentBroker : IPaymentBroker
    {
        private readonly IPaymentProviders paymentProviders;

        public PaymentBroker(IPaymentProviders paymentProviders)
        {
            this.paymentProviders = paymentProviders;
        }

        
    }
}

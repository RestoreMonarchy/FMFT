using FMFT.Extensions.Payments.Models.Arguments;
using FMFT.Extensions.Payments.Models.Enums;
using FMFT.Extensions.Payments.Models.Results;
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

        public async ValueTask<RegisterPaymentResult> RegisterPaymentAsync(PaymentProviderId paymentProviderId, RegisterPaymentArguments arguments)
        {
            RegisterPaymentResult result = await paymentProviders.RegisterPaymentAsync(paymentProviderId, arguments);

            return result;
        }

        public async ValueTask<GetPaymentUrlResult> GetPaymentUrlAsync(PaymentProviderId paymentProviderId, GetPaymentUrlArguments arguments)
        {
            GetPaymentUrlResult result = await paymentProviders.GetPaymentUrlAsync(paymentProviderId, arguments);

            return result;
        }
    }
}

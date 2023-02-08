using FMFT.Extensions.Payments.Models.Arguments;
using FMFT.Extensions.Payments.Models.Enums;
using FMFT.Extensions.Payments.Models.Exceptions;
using FMFT.Extensions.Payments.Models.Results;

namespace FMFT.Extensions.Payments.Services
{
    public class PaymentProviders : IPaymentProviders
    {
        private readonly IEnumerable<IPaymentProvider> paymentProviders;

        public PaymentProviders(IEnumerable<IPaymentProvider> paymentProviders)
        {
            this.paymentProviders = paymentProviders;
        }

        public ValueTask<ProcessPaymentNotificationResult> ProcessPaymentNotificationAsync(
            PaymentProviderId paymentProviderId, 
            ProcessPaymentNotificationArguments arguments)
        {
            IPaymentProvider paymentProvider = GetPaymentProvider(paymentProviderId);

            return paymentProvider.ProcessPaymentNotificationAsync(arguments);
        }

        public ValueTask<GetPaymentInfoResult> GetPaymentInfoAsync(PaymentProviderId paymentProviderId, GetPaymentInfoArguments arguments)
        {
            IPaymentProvider paymentProvider = GetPaymentProvider(paymentProviderId);

            return paymentProvider.GetPaymentInfoAsync(arguments);
        }

        public ValueTask<GetPaymentUrlResult> GetPaymentUrlAsync(PaymentProviderId paymentProviderId, GetPaymentUrlArguments arguments)
        {
            IPaymentProvider paymentProvider = GetPaymentProvider(paymentProviderId);

            return paymentProvider.GetPaymentUrlAsync(arguments);
        }

        public ValueTask<RegisterPaymentResult> RegisterPaymentAsync(PaymentProviderId paymentProviderId, 
            PaymentMethodId paymentMethodId, 
            RegisterPaymentArguments arguments)
        {
            IPaymentProvider paymentProvider = GetPaymentProvider(paymentProviderId);

            if (!paymentProvider.SupportedPaymentMethodIds.Contains(paymentMethodId))
            {
                throw new PaymentMethodNotSupportedException(paymentMethodId, paymentProviderId);
            }

            return paymentProvider.RegisterPaymentAsync(paymentMethodId, arguments);
        }

        private IPaymentProvider GetPaymentProvider(PaymentProviderId paymentProvider)
        {
            IPaymentProvider provider = paymentProviders.FirstOrDefault(x => x.Id == paymentProvider);
            
            if (provider == null)
            {
                throw new PaymentProviderNotSupportedException(paymentProvider);
            }

            return provider;
        }
    }
}

using FMFT.Extensions.Payments.Models.Arguments;
using FMFT.Extensions.Payments.Models.Enums;
using FMFT.Extensions.Payments.Models.Results;

namespace FMFT.Extensions.Payments.Services
{
    public interface IPaymentProviders
    {
        ValueTask<RegisterPaymentResult> RegisterPaymentAsync(PaymentProviderId paymentProviderId, PaymentMethodId paymentMethodId, RegisterPaymentArguments arguments);
        ValueTask<GetPaymentUrlResult> GetPaymentUrlAsync(PaymentProviderId paymentProviderId, GetPaymentUrlArguments arguments);
        ValueTask<GetPaymentInfoResult> GetPaymentInfoAsync(PaymentProviderId paymentProviderId, GetPaymentInfoArguments arguments);
        ValueTask<ProcessPaymentNotificationResult> ProcessPaymentNotificationAsync(PaymentProviderId paymentProviderId, ProcessPaymentNotificationArguments arguments);
    }
}
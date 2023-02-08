using FMFT.Extensions.Payments.Models.Arguments;
using FMFT.Extensions.Payments.Models.Enums;
using FMFT.Extensions.Payments.Models.Results;

namespace FMFT.Extensions.Payments.Services
{
    public interface IPaymentProvider
    {
        PaymentProviderId Id { get; }
        PaymentMethodId[] SupportedPaymentMethodIds { get; }
        ValueTask<GetPaymentInfoResult> GetPaymentInfoAsync(GetPaymentInfoArguments arguments);
        ValueTask<GetPaymentUrlResult> GetPaymentUrlAsync(GetPaymentUrlArguments arguments);
        ValueTask<RegisterPaymentResult> RegisterPaymentAsync(PaymentMethodId paymentMethodId, RegisterPaymentArguments arguments);
        ValueTask<ProcessPaymentNotificationResult> ProcessPaymentNotificationAsync(ProcessPaymentNotificationArguments arguments);
    }
}

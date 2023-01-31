using FMFT.Extensions.Payments.Models.Arguments;
using FMFT.Extensions.Payments.Models.Enums;
using FMFT.Extensions.Payments.Models.Results;

namespace FMFT.Web.Server.Brokers.Payments
{
    public interface IPaymentBroker
    {
        ValueTask<GetPaymentInfoResult> GetPaymentInfoAsync(PaymentProviderId paymentProviderId, GetPaymentInfoArguments arguments);
        ValueTask<GetPaymentUrlResult> GetPaymentUrlAsync(PaymentProviderId paymentProviderId, GetPaymentUrlArguments arguments);
        ValueTask<ProcessPaymentNotificationResult> ProcessPaymentNotificationAsync(PaymentProviderId paymentProviderId, ProcessPaymentNotificationArguments arguments);
        ValueTask<RegisterPaymentResult> RegisterPaymentAsync(PaymentProviderId paymentProviderId, RegisterPaymentArguments arguments);
    }
}
using FMFT.Extensions.Payments.Models.Arguments;
using FMFT.Extensions.Payments.Models.Enums;
using FMFT.Extensions.Payments.Models.Results;

namespace FMFT.Web.Server.Brokers.Payments
{
    public interface IPaymentBroker
    {
        ValueTask<GetPaymentUrlResult> GetPaymentUrlAsync(PaymentProviderId paymentProviderId, GetPaymentUrlArguments arguments);
        ValueTask<RegisterPaymentResult> RegisterPaymentAsync(PaymentProviderId paymentProviderId, RegisterPaymentArguments arguments);
    }
}
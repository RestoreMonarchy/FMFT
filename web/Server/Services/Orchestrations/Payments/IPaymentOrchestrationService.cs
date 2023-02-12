using FMFT.Web.Server.Models.Payments.Params;
using FMFT.Web.Server.Models.Payments;
using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Server.Services.Orchestrations.Payments
{
    public interface IPaymentOrchestrationService
    {
        ValueTask<PaymentInfo> GetPaymentInfoAsync(GetPaymentInfoParams @params);
        PaymentProvider GetPaymentProviderFromPaymentMethod(PaymentMethod paymentMethod);
        ValueTask<PaymentUrl> GetPaymentUrlAsync(GetPaymentUrlParams @params);
        ValueTask<ProcessedPayment> ProcessPaymentNotificationAsync(PaymentProvider paymentProvider);
        ValueTask<RegisteredPayment> RegisterPaymentAsync(RegisterPaymentParams @params);
    }
}
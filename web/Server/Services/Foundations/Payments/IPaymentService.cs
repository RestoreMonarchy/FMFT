using FMFT.Web.Server.Models.Payments;
using FMFT.Web.Server.Models.Payments.Params;
using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Server.Services.Foundations.Payments
{
    public interface IPaymentService
    {
        ValueTask<PaymentInfo> GetPaymentInfoAsync(GetPaymentInfoParams @params);
        PaymentProvider GetPaymentProviderFromPaymentMethod(PaymentMethod paymentMethod);
        ValueTask<PaymentUrl> GetPaymentUrlAsync(GetPaymentUrlParams @params);
        ValueTask<ProcessedPayment> ProcessPaymentNotificationAsync(ProcessPaymentNotificationParams @params);
        ValueTask<RegisteredPayment> RegisterPaymentAsync(RegisterPaymentParams @params);
    }
}

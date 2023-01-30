using FMFT.Web.Server.Models.Payments.Params;
using FMFT.Web.Server.Models.Payments;

namespace FMFT.Web.Server.Services.Orchestrations.Payments
{
    public interface IPaymentOrchestrationService
    {
        ValueTask<PaymentUrl> GetPaymentUrlAsync(GetPaymentUrlParams @params);
        ValueTask<RegisteredPayment> RegisterPaymentAsync(RegisterPaymentParams @params);
    }
}
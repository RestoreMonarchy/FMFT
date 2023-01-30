using FMFT.Web.Server.Models.Payments;
using FMFT.Web.Server.Models.Payments.Params;

namespace FMFT.Web.Server.Services.Foundations.Payments
{
    public interface IPaymentService
    {
        ValueTask<PaymentUrl> GetPaymentUrlAsync(GetPaymentUrlParams @params);
        ValueTask<RegisteredPayment> RegisterPaymentAsync(RegisterPaymentParams @params);
    }
}

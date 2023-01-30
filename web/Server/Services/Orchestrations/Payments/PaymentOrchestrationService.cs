using FMFT.Web.Server.Models.Payments;
using FMFT.Web.Server.Models.Payments.Params;
using FMFT.Web.Server.Services.Foundations.Payments;

namespace FMFT.Web.Server.Services.Orchestrations.Payments
{
    public class PaymentOrchestrationService : IPaymentOrchestrationService
    {
        private readonly IPaymentService paymentService;

        public PaymentOrchestrationService(IPaymentService paymentService)
        {
            this.paymentService = paymentService;
        }

        public async ValueTask<PaymentUrl> GetPaymentUrlAsync(GetPaymentUrlParams @params)
        {
            return await paymentService.GetPaymentUrlAsync(@params);
        }

        public async ValueTask<RegisteredPayment> RegisterPaymentAsync(RegisterPaymentParams @params)
        {
            return await paymentService.RegisterPaymentAsync(@params);
        }
    }
}

using FMFT.Extensions.Payments.Models.Arguments;
using FMFT.Extensions.Payments.Models.Enums;
using FMFT.Extensions.Payments.Models.Exceptions;
using FMFT.Extensions.Payments.Models.Results;
using FMFT.Web.Server.Brokers.Payments;
using FMFT.Web.Server.Models.Payments;
using FMFT.Web.Server.Models.Payments.Exceptions;
using FMFT.Web.Server.Models.Payments.Params;
using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Server.Services.Foundations.Payments
{
    public partial class PaymentService : IPaymentService
    {
        private readonly IPaymentBroker paymentBroker;
        private readonly IHttpContextAccessor httpContextAccessor;

        public PaymentService(IPaymentBroker paymentBroker, IHttpContextAccessor httpContextAccessor)
        {
            this.paymentBroker = paymentBroker;
            this.httpContextAccessor = httpContextAccessor;
        }

        public PaymentProvider GetPaymentProviderFromPaymentMethod(PaymentMethod paymentMethod)
        {
            if (paymentMethod is PaymentMethod.Mock)
            {
                return PaymentProvider.Mock;
            }

            if (paymentMethod is PaymentMethod.Blik or PaymentMethod.Przelewy24 or PaymentMethod.CreditDebitCard)
            {
                return PaymentProvider.Przelewy24;
            }

            throw new NotSupportedPaymentMethodException();
        }

        public async ValueTask<PaymentInfo> GetPaymentInfoAsync(GetPaymentInfoParams @params)
        {
            GetPaymentInfoArguments arguments = MapGetPaymentInfoParamsToArguments(@params);
            PaymentProviderId paymentProviderId = MapPaymentMethodToPaymentProviderId(@params.PaymentMethod);            

            GetPaymentInfoResult result = await paymentBroker.GetPaymentInfoAsync(paymentProviderId, arguments);

            return MapGetPaymentInfoResultToPaymentInfo(result);
        }

        public async ValueTask<ProcessedPayment> ProcessPaymentNotificationAsync(ProcessPaymentNotificationParams @params)
        {
            HttpContext httpContext = httpContextAccessor.HttpContext;

            ProcessPaymentNotificationArguments arguments = new()
            {
                HttpContext = httpContext
            };
            PaymentProviderId paymentProviderId = MapPaymentProviderToPaymentProviderId(@params.PaymentProvider);

            ProcessPaymentNotificationResult result = await paymentBroker.ProcessPaymentNotificationAsync(paymentProviderId, arguments);

            return MapProcessPaymentNotificationResultToProcessedPayment(result);
        }

        public async ValueTask<RegisteredPayment> RegisterPaymentAsync(RegisterPaymentParams @params)
        {
            RegisterPaymentArguments arguments = MapRegisterPaymentParamsToArguments(@params);
            PaymentProviderId paymentProviderId = MapPaymentMethodToPaymentProviderId(@params.PaymentMethod);
            PaymentMethodId paymentMethodId = MapPaymentMethodToPaymentMethodId(@params.PaymentMethod);

            try
            {
                RegisterPaymentResult result = await paymentBroker.RegisterPaymentAsync(paymentProviderId, paymentMethodId, arguments);

                return MapRegisterPaymentResultToRegisteredPayment(result);
            } catch (PaymentProviderNotSupportedException)
            {
                throw new NotSupportedPaymentProviderException();
            }            
        }

        public async ValueTask<PaymentUrl> GetPaymentUrlAsync(GetPaymentUrlParams @params)
        {
            GetPaymentUrlArguments arguments = MapGetPaymentUrlParamsToArguments(@params);
            PaymentProviderId paymentProviderId = MapPaymentMethodToPaymentProviderId(@params.PaymentMethod);

            try
            {
                GetPaymentUrlResult result = await paymentBroker.GetPaymentUrlAsync(paymentProviderId, arguments);

                return MapGetPaymentUrlResultToPaymentUrl(result);
            } catch (PaymentProviderNotSupportedException)
            {
                throw new NotSupportedPaymentProviderException();
            }            
        }        
    }
}

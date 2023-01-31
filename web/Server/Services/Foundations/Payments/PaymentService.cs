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
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentBroker paymentBroker;
        private readonly IHttpContextAccessor httpContextAccessor;

        public PaymentService(IPaymentBroker paymentBroker, IHttpContextAccessor httpContextAccessor)
        {
            this.paymentBroker = paymentBroker;
            this.httpContextAccessor = httpContextAccessor;
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
                Request = httpContext.Request
            };
            PaymentProviderId paymentProviderId = MapPaymentMethodToPaymentProviderId(@params.PaymentMethod);

            ProcessPaymentNotificationResult result = await paymentBroker.ProcessPaymentNotificationAsync(paymentProviderId, arguments);

            return MapProcessPaymentNotificationResultToProcessedPayment(result);
        }

        public async ValueTask<RegisteredPayment> RegisterPaymentAsync(RegisterPaymentParams @params)
        {
            RegisterPaymentArguments arguments = MapRegisterPaymentParamsToArguments(@params);
            PaymentProviderId paymentProviderId = MapPaymentMethodToPaymentProviderId(@params.PaymentMethod);

            try
            {
                RegisterPaymentResult result = await paymentBroker.RegisterPaymentAsync(paymentProviderId, arguments);

                return MapRegisterPaymentResultToRegisteredPayment(result);
            } catch (PaymentProviderNotSupportedException)
            {
                throw new NotSupportedProviderPaymentException();
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
                throw new NotSupportedProviderPaymentException();
            }            
        }

        private PaymentInfo MapGetPaymentInfoResultToPaymentInfo(GetPaymentInfoResult result)
        {
            return new()
            {
                PaymentStatus = MapPaymentStatusIdToPaymentStatus(result.PaymentStatus)
            };
        }

        private ProcessedPayment MapProcessPaymentNotificationResultToProcessedPayment(ProcessPaymentNotificationResult result)
        {
            return new()
            {
                PaymentStatus = MapPaymentStatusIdToPaymentStatus(result.PaymentStatus)
            };
        }

        private PaymentUrl MapGetPaymentUrlResultToPaymentUrl(GetPaymentUrlResult result)
        {
            return new()
            {
                Url = result.Url
            };
        }

        private GetPaymentUrlArguments MapGetPaymentUrlParamsToArguments(GetPaymentUrlParams @params)
        {
            return new()
            {
                PaymentToken = @params.PaymentToken,
                SessionId = @params.SessionId
            };
        }
        
        private GetPaymentInfoArguments MapGetPaymentInfoParamsToArguments(GetPaymentInfoParams @params)
        {
            return new()
            {
                PaymentToken = @params.PaymentToken,
                SessionId = @params.SessionId
            };
        }                

        private PaymentStatus MapPaymentStatusIdToPaymentStatus(PaymentStatusId paymentStatusId)
        {
            return (PaymentStatus)paymentStatusId;
        }

        private PaymentProviderId MapPaymentMethodToPaymentProviderId(PaymentMethod paymentMethod)
        {
            if (paymentMethod is PaymentMethod.Mock)
            {
                return PaymentProviderId.Mock;
            }

            if (paymentMethod is PaymentMethod.Blik or PaymentMethod.Przelewy24 or PaymentMethod.CreditDebitCard)
            {
                return PaymentProviderId.Przelewy24;
            }

            throw new NotImplementedException($"Payment method {paymentMethod} does not have a matching provider");
        }

        private RegisteredPayment MapRegisterPaymentResultToRegisteredPayment(RegisterPaymentResult result)
        {
            return new RegisteredPayment()
            {
                Token = result.Token
            };
        }

        private RegisterPaymentArguments MapRegisterPaymentParamsToArguments(RegisterPaymentParams @params)
        {
            RegisterPaymentArguments arguments = new()
            {
                OrderId = @params.OrderId,
                SessionId = @params.SessionId,
                Amount = @params.Amount,
                Currency = @params.Currency,
                CustomerEmailAddress = @params.CustomerEmailAddress,
                CustomerFirstName = @params.CustomerFirstName,
                CustomerLastName = @params.CustomerLastName,
                CustomerId = @params.CustomerId,
                ExpireDate = @params.ExpireDate,
                Items = new()
            };

            foreach (RegisterPaymentParams.Item item in @params.Items)
            {
                arguments.Items.Add(new RegisterPaymentArguments.Item()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Price = item.Price,
                    Quantity = item.Quantity
                });
            }

            return arguments;
        }
    }
}

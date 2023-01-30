using FMFT.Extensions.Payments.Models.Arguments;
using FMFT.Extensions.Payments.Models.Enums;
using FMFT.Extensions.Payments.Models.Results;
using FMFT.Web.Server.Brokers.Payments;
using FMFT.Web.Server.Models.Payments;
using FMFT.Web.Server.Models.Payments.Params;
using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Server.Services.Foundations.Payments
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentBroker paymentBroker;

        public PaymentService(IPaymentBroker paymentBroker)
        {
            this.paymentBroker = paymentBroker;
        }

        public async ValueTask<RegisteredPayment> RegisterPaymentAsync(RegisterPaymentParams @params)
        {
            RegisterPaymentArguments arguments = MapRegisterPaymentParamsToArguments(@params);
            PaymentProviderId paymentProviderId = MapPaymentMethodToPaymentProviderId(@params.PaymentMethod);

            RegisterPaymentResult result = await paymentBroker.RegisterPaymentAsync(paymentProviderId, arguments);

            return MapRegisterPaymentResultToRegisteredPayment(result);
        }

        public async ValueTask<PaymentUrl> GetPaymentUrlAsync(GetPaymentUrlParams @params)
        {
            GetPaymentUrlArguments arguments = MapGetPaymentUrlParamsToArguments(@params);
            PaymentProviderId paymentProviderId = MapPaymentMethodToPaymentProviderId(@params.PaymentMethod);

            GetPaymentUrlResult result = await paymentBroker.GetPaymentUrlAsync(paymentProviderId, arguments);

            return MapGetPaymentUrlResultToPaymentUrl(result);
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

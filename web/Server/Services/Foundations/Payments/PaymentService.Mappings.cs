using FMFT.Extensions.Payments.Models.Arguments;
using FMFT.Extensions.Payments.Models.Enums;
using FMFT.Extensions.Payments.Models.Results;
using FMFT.Web.Server.Models.Payments.Params;
using FMFT.Web.Server.Models.Payments;
using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Server.Services.Foundations.Payments
{
    public partial class PaymentService
    {
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
                PaymentStatus = MapPaymentStatusIdToPaymentStatus(result.PaymentStatus),
                SessionId = Guid.Parse(result.SessionId)
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

        private PaymentProviderId MapPaymentProviderToPaymentProviderId(PaymentProvider paymentProvider)
        {
            switch (paymentProvider)
            {
                case PaymentProvider.Mock:
                    return PaymentProviderId.Mock;
                case PaymentProvider.Przelewy24:
                    return PaymentProviderId.Przelewy24;
                default:
                    return PaymentProviderId.None;
            }
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

        private PaymentMethodId MapPaymentMethodToPaymentMethodId(PaymentMethod paymentMethod)
        {
            return paymentMethod switch
            {
                PaymentMethod.Mock => PaymentMethodId.Mock,
                PaymentMethod.None => PaymentMethodId.None,
                PaymentMethod.CreditDebitCard => PaymentMethodId.CreditDebitCard,
                PaymentMethod.Przelewy24 => PaymentMethodId.Przelewy24,
                PaymentMethod.Blik => PaymentMethodId.Blik,
                _ => PaymentMethodId.None,
            };
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

﻿using FMFT.Extensions.Payments.Models.Arguments;
using FMFT.Extensions.Payments.Models.Enums;
using FMFT.Extensions.Payments.Models.Results;
using FMFT.Extensions.Payments.Services;

namespace FMFT.Web.Server.Brokers.Payments
{
    public class PaymentBroker : IPaymentBroker
    {
        private readonly IPaymentProviders paymentProviders;

        public PaymentBroker(IPaymentProviders paymentProviders)
        {
            this.paymentProviders = paymentProviders;
        }

        public async ValueTask<RegisterPaymentResult> RegisterPaymentAsync(PaymentProviderId paymentProviderId, 
            PaymentMethodId paymentMethodId,
            RegisterPaymentArguments arguments)
        {
            RegisterPaymentResult result = await paymentProviders.RegisterPaymentAsync(paymentProviderId, paymentMethodId, arguments);

            return result;
        }

        public async ValueTask<GetPaymentUrlResult> GetPaymentUrlAsync(PaymentProviderId paymentProviderId, GetPaymentUrlArguments arguments)
        {
            GetPaymentUrlResult result = await paymentProviders.GetPaymentUrlAsync(paymentProviderId, arguments);

            return result;
        }

        public async ValueTask<GetPaymentInfoResult> GetPaymentInfoAsync(PaymentProviderId paymentProviderId, GetPaymentInfoArguments arguments)
        {
            GetPaymentInfoResult result = await paymentProviders.GetPaymentInfoAsync(paymentProviderId, arguments);

            return result;
        }

        public async ValueTask<ProcessPaymentNotificationResult> ProcessPaymentNotificationAsync(
            PaymentProviderId paymentProviderId,
            ProcessPaymentNotificationArguments arguments)
        {
            ProcessPaymentNotificationResult result = await paymentProviders.ProcessPaymentNotificationAsync(paymentProviderId, arguments);

            return result;
        }
    }
}

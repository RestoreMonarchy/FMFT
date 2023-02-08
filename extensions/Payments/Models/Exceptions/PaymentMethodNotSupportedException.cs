using FMFT.Extensions.Payments.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMFT.Extensions.Payments.Models.Exceptions
{
    internal class PaymentMethodNotSupportedException : NotSupportedException
    {
        public PaymentMethodNotSupportedException(PaymentMethodId paymentMethodId, PaymentProviderId paymentProviderId)
            : base($"Payment method {paymentMethodId} is not supported in {paymentProviderId}")
        {

        }
    }
}

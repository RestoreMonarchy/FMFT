using FMFT.Extensions.Payments.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMFT.Extensions.Payments.Models.Exceptions
{
    public class PaymentProviderNotSupportedException : NotSupportedException
    {
        public PaymentProviderNotSupportedException(PaymentProviderId paymentProvider)
            : base($"Payment provider {paymentProvider} is not supported")
        {

        }
    }
}

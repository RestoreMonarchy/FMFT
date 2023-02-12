using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMFT.Extensions.Payments.Models.Arguments
{
    public class GetPaymentInfoArguments
    {
        public string SessionId { get; set; }
        public string PaymentToken { get; set; }
    }
}

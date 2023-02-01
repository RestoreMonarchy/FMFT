using FMFT.Extensions.Payments.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMFT.Extensions.Payments.Models.Results
{
    public class ProcessPaymentNotificationResult
    {
        public PaymentStatusId PaymentStatus { get; set; }
        public string SessionId { get; set; }
    }
}

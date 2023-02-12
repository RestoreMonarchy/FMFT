using Microsoft.AspNetCore.Http;

namespace FMFT.Extensions.Payments.Models.Arguments
{
    public class ProcessPaymentNotificationArguments
    {
        public HttpContext HttpContext { get; set; }
    }
}

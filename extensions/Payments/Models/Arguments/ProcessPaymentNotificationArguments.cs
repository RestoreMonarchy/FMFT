using Microsoft.AspNetCore.Http;

namespace FMFT.Extensions.Payments.Models.Arguments
{
    public class ProcessPaymentNotificationArguments
    {
        public HttpRequest Request { get; set; }
    }
}

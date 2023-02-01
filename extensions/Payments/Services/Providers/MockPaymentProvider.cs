using FMFT.Extensions.Payments.Extensions;
using FMFT.Extensions.Payments.Models.Arguments;
using FMFT.Extensions.Payments.Models.Enums;
using FMFT.Extensions.Payments.Models.Options;
using FMFT.Extensions.Payments.Models.Results;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;

namespace FMFT.Extensions.Payments.Services.Providers
{
    public class MockPaymentProvider : IPaymentProvider
    {
        public PaymentProviderId Id => PaymentProviderId.Mock;

        private readonly PaymentProviderOptions options;

        public MockPaymentProvider(IOptions<PaymentProviderOptions> options)
        {
            this.options = options.Value;
        }

        public ValueTask<RegisterPaymentResult> RegisterPaymentAsync(RegisterPaymentArguments arguments)
        {
            RegisterPaymentResult result = new()
            {
                Token = Guid.NewGuid().ToString()
            };

            return ValueTask.FromResult(result);
        }

        public ValueTask<GetPaymentUrlResult> GetPaymentUrlAsync(GetPaymentUrlArguments arguments)
        {
            GetPaymentUrlResult result = new()
            {
                Url = options.MockPaymentUrl
            };

            return ValueTask.FromResult(result);
        }

        public async ValueTask<ProcessPaymentNotificationResult> ProcessPaymentNotificationAsync(ProcessPaymentNotificationArguments arguments)
        {
            string json = await arguments.HttpContext.Request.ReadBodyToStringAsync();

            JObject jObject = JObject.Parse(json);

            ProcessPaymentNotificationResult result = new()
            {
                PaymentStatus = PaymentStatusId.Completed,
                SessionId = jObject["SessionId"].Value<string>()
            };

            return result;
        }

        public ValueTask<GetPaymentInfoResult> GetPaymentInfoAsync(GetPaymentInfoArguments arguments)
        {
            GetPaymentInfoResult result = new()
            {
                PaymentStatus = PaymentStatusId.Completed
            };

            return ValueTask.FromResult(result);
        }
    }
}

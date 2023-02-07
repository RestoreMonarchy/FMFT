using FMFT.Extensions.Payments.Models.Arguments;
using FMFT.Extensions.Payments.Models.Enums;
using FMFT.Extensions.Payments.Models.Options;
using FMFT.Extensions.Payments.Models.Results;
using Microsoft.Extensions.Options;

namespace FMFT.Extensions.Payments.Services.Providers
{
    public class Przelewy24PaymentProvider : IPaymentProvider
    {
        public PaymentProviderId Id => PaymentProviderId.Przelewy24;

        private readonly Przelewy24Options options;

        public Przelewy24PaymentProvider(IOptions<Przelewy24Options> options)
        {
            this.options = options.Value;
        }

        public ValueTask<GetPaymentInfoResult> GetPaymentInfoAsync(GetPaymentInfoArguments arguments)
        {
            GetPaymentInfoResult result = new();

            return ValueTask.FromResult(result);
        }

        public ValueTask<GetPaymentUrlResult> GetPaymentUrlAsync(GetPaymentUrlArguments arguments)
        {
            GetPaymentUrlResult result = new()
            {
                Url = $"{options.Username}"
            };

            return ValueTask.FromResult(result);
        }

        public ValueTask<ProcessPaymentNotificationResult> ProcessPaymentNotificationAsync(ProcessPaymentNotificationArguments arguments)
        {
            ProcessPaymentNotificationResult result = new()
            {
            };

            return ValueTask.FromResult(result);
        }

        public ValueTask<RegisterPaymentResult> RegisterPaymentAsync(RegisterPaymentArguments arguments)
        {
            RegisterPaymentResult result = new()
            {
                Token = Guid.NewGuid().ToString()
            };

            return ValueTask.FromResult(result);
        }
    }
}

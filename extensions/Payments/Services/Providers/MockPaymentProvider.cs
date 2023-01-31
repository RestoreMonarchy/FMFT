using FMFT.Extensions.Payments.Models.Arguments;
using FMFT.Extensions.Payments.Models.Enums;
using FMFT.Extensions.Payments.Models.Options;
using FMFT.Extensions.Payments.Models.Results;
using Microsoft.Extensions.Options;

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
    }
}

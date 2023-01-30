using FMFT.Extensions.Payments.Models.Arguments;
using FMFT.Extensions.Payments.Models.Enums;
using FMFT.Extensions.Payments.Models.Results;

namespace FMFT.Extensions.Payments.Services.Providers
{
    public class MockPaymentProvider : IPaymentProvider
    {
        public PaymentProviderId Id => PaymentProviderId.Mock;

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
                Url = ""
            };

            return ValueTask.FromResult(result);
        }
    }
}

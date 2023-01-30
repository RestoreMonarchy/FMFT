using Microsoft.Extensions.DependencyInjection;

namespace FMFT.Extensions.Payments.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static PaymentProvidersBuilder AddPaymentProviders(this IServiceCollection services)
        {
            PaymentProvidersBuilder builder = new(services);

            return builder.AddBase();
        }
    }
}

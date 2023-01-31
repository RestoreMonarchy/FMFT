using FMFT.Extensions.Payments.Models.Options;
using Microsoft.Extensions.DependencyInjection;

namespace FMFT.Extensions.Payments.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static PaymentProvidersBuilder AddPaymentProviders(this IServiceCollection services, Action<PaymentProviderOptions> configureOptions)
        {
            PaymentProvidersBuilder builder = new(services);

            return builder.ConfigureOptions(configureOptions).AddBase();
        }
    }
}

using FMFT.Extensions.Payments.Models.Options;
using FMFT.Extensions.Payments.Services;
using FMFT.Extensions.Payments.Services.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace FMFT.Extensions.Payments
{
    public class PaymentProvidersBuilder
    {
        private readonly IServiceCollection services;

        public PaymentProvidersBuilder(IServiceCollection services)
        {
            this.services = services;
        }

        internal PaymentProvidersBuilder ConfigureOptions(Action<PaymentProviderOptions> configureOptions)
        {
            services.Configure(configureOptions);

            return this;
        }

        internal PaymentProvidersBuilder AddBase()
        {
            services.AddTransient<IPaymentProviders, PaymentProviders>();

            return this;
        }

        public PaymentProvidersBuilder AddMock()
        {
            services.AddTransient<IPaymentProvider, MockPaymentProvider>();

            return this;
        }

        public PaymentProvidersBuilder AddPrzelewy24(Action<Przelewy24Options> configureOptions)
        {
            services.Configure(configureOptions);

            services.AddTransient<IPaymentProvider, Przelewy24PaymentProvider>();

            return this;
        }
    }
}

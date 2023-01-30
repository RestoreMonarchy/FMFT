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
    }
}

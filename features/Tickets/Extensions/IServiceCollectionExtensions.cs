using FMFT.Features.Tickets.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FMFT.Features.Tickets.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddTicketsFeatures(this IServiceCollection services)
        {
            services.AddTransient<TicketGenerator>();

            return services;
        }
    }
}

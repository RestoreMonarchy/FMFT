using FMFT.Web.Server.Brokers.Storages;
using FMFT.Web.Server.Services.Foundations.Seats;

namespace FMFT.Web.Server.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddBrokers(this IServiceCollection services)
        {
            services.AddScoped<IStorageBroker, StorageBroker>();
            return services;
        }

        public static IServiceCollection AddFoundations(this IServiceCollection services)
        {
            services.AddTransient<ISeatService, SeatService>();
            return services;
        }
    }
}

using FMFT.Extensions.Blazor.Google.Models.Options;
using FMFT.Extensions.Blazor.Google.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FMFT.Extensions.Blazor.Google.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddGoogle(this IServiceCollection services, Action<GoogleOptions> options)
        {
            services.Configure(options);

            services.AddScoped<GoogleService>();

            return services;
        }
    }
}

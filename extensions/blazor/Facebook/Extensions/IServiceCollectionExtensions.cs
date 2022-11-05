using FMFT.Extensions.Blazor.Facebook.Models.Options;
using FMFT.Extensions.Blazor.Facebook.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FMFT.Extensions.Blazor.Facebook.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddFacebook(this IServiceCollection services, Action<FacebookOptions> options)
        {
            services.Configure(options);

            services.AddScoped<FacebookService>();

            return services;
        }
    }
}

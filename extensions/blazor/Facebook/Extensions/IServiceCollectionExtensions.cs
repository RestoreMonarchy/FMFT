using FMFT.Extensions.Blazor.Facebook.Models;
using Microsoft.Extensions.DependencyInjection;

namespace FMFT.Extensions.Blazor.Facebook.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddFacebook(this IServiceCollection services, Action<FacebookOptions> options)
        {
            services.Configure(options);
            services.AddSingleton<FacebookService>();

            return services;
        }
    }
}

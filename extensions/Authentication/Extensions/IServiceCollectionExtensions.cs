using FMFT.Extensions.Authentication.Constants;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace FMFT.Extensions.Authentication.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static AuthenticationBuilder AddDefaultAuthentication(this IServiceCollection services)
        {
            return services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = FMFTAuthenticationDefaults.ApplicationScheme;
                options.DefaultChallengeScheme = FMFTAuthenticationDefaults.ApplicationScheme;
                options.DefaultSignInScheme = FMFTAuthenticationDefaults.ExternalScheme;
            });
        }
    }
}

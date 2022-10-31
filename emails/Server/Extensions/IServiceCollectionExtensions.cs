using FMFT.Emails.Server.Services;
using Microsoft.Extensions.DependencyInjection;
using RestoreMonarchy.RazorViewEmailTemplates.Extensions;

namespace FMFT.Emails.Server.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddServerEmailGenerator(this IServiceCollection services)
        {
            services.AddEmailHtmlGenerator();
            services.AddTransient<ServerEmailGenerator>();

            return services;
        }
    }
}

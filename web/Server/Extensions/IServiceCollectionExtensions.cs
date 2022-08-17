using FMFT.Web.Server.Brokers.Authentications;
using FMFT.Web.Server.Brokers.Encryptions;
using FMFT.Web.Server.Brokers.Storages;
using FMFT.Web.Server.Brokers.Validations;
using FMFT.Web.Server.Services.Implementations.Cookies;
using FMFT.Web.Server.Services.Foundations.Auditoriums;
using FMFT.Web.Server.Services.Foundations.Seats;
using FMFT.Web.Server.Services.Foundations.Shows;
using FMFT.Web.Server.Services.Foundations.Users;
using FMFT.Web.Server.Services.Processings.Users;
using FMFT.Web.Server.Services.Foundations.Reservations;
using FMFT.Web.Server.Services.Processings.Reservations;
using FMFT.Web.Server.Brokers.Urls;
using FMFT.Extensions.Authentication.Constants;
using FMFT.Extensions.Authentication.Extensions;
using Microsoft.AspNetCore.Authentication;
using FMFT.Web.Server.Services.Orchestrations.Reservations;
using FMFT.Web.Server.Services.Foundations.Accounts;
using FMFT.Web.Server.Services.Processings.Accounts;
using FMFT.Web.Server.Services.Orchestrations.UserAccounts;
using FMFT.Web.Server.Services.Processings.Shows;
using FMFT.Web.Server.Services.Orchestrations.AccountShows;

namespace FMFT.Web.Server.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddFMFTAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            AuthenticationBuilder authenticationBuilder = services.AddDefaultAuthentication()
                .AddCookie(FMFTAuthenticationDefaults.ApplicationScheme, o =>
                {
                    o.Cookie.Name = FMFTAuthenticationDefaults.ApplicationScheme;
                    o.ExpireTimeSpan = TimeSpan.FromHours(24);
                    o.EventsType = typeof(CustomCookieAuthenticationEvents);
                })
                .AddCookie(FMFTAuthenticationDefaults.ExternalScheme, o =>
                {
                    o.Cookie.Name = FMFTAuthenticationDefaults.ExternalScheme;
                    o.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                });
            
            if (configuration.GetSection("Authentication").GetSection("Google").GetValue<bool>("Enabled"))
            {
                authenticationBuilder.AddGoogle(options =>
                {
                    options.ClientId = configuration.GetSection("Authentication").GetSection("Google")["ClientId"];
                    options.ClientSecret = configuration.GetSection("Authentication").GetSection("Google")["ClientSecret"];
                });
            }

            if (configuration.GetSection("Authentication").GetSection("Facebook").GetValue<bool>("Enabled"))
            {
                authenticationBuilder.AddFacebook(options =>
                {
                    options.AppId = configuration.GetSection("Authentication").GetSection("Facebook")["AppId"];
                    options.AppSecret = configuration.GetSection("Authentication").GetSection("Facebook")["AppSecret"];
                });
            }

            return services;
        }

        public static IServiceCollection AddBrokers(this IServiceCollection services)
        {
            services.AddScoped<IStorageBroker, StorageBroker>();
            services.AddScoped<IAuthenticationBroker, AuthenticationBroker>();
            services.AddScoped<IEncryptionBroker, EncryptionBroker>();
            services.AddScoped<IValidationBroker, ValidationBroker>();
            services.AddScoped<IUrlBroker, UrlBroker>();
            return services;
        }

        public static IServiceCollection AddFoundations(this IServiceCollection services)
        {
            services.AddTransient<ISeatService, SeatService>();
            services.AddTransient<IAuditoriumService, AuditoriumService>();
            services.AddTransient<IShowService, ShowService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IReservationService, ReservationService>();
            services.AddTransient<IAccountService, AccountService>();
            return services;
        }

        public static IServiceCollection AddProcessings(this IServiceCollection services)
        {
            services.AddTransient<IUserProcessingService, UserProcessingService>();
            services.AddTransient<IReservationProcessingService, ReservationProcessingService>();
            services.AddTransient<IAccountProcessingService, AccountProcessingService>();
            services.AddTransient<IShowProcessingService, ShowProcessingService>();
            return services;
        }

        public static IServiceCollection AddOrchestrations(this IServiceCollection services)
        {
            services.AddTransient<IReservationOrchestrationService, ReservationOrchestrationService>();
            services.AddTransient<IUserAccountOrchestrationService, UserAccountOrchestrationService>();
            services.AddTransient<IAccountShowOrchestrationService, AccountShowOrchestrationService>();
            return services;
        }

        public static IServiceCollection AddImplementations(this IServiceCollection services)
        {
            services.AddTransient<CustomCookieAuthenticationEvents>();
            return services;
        }
    }
}

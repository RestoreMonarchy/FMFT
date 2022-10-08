using FMFT.Extensions.Authentication.Constants;
using FMFT.Web.Server.Brokers.Authentications;
using FMFT.Web.Server.Brokers.Encryptions;
using FMFT.Web.Server.Brokers.Loggings;
using FMFT.Web.Server.Brokers.Storages;
using FMFT.Web.Server.Brokers.Urls;
using FMFT.Web.Server.Brokers.Validations;
using FMFT.Web.Server.Models.Options.Authentications;
using FMFT.Web.Server.Services.Foundations.Accounts;
using FMFT.Web.Server.Services.Foundations.Auditoriums;
using FMFT.Web.Server.Services.Foundations.Reservations;
using FMFT.Web.Server.Services.Foundations.Seats;
using FMFT.Web.Server.Services.Foundations.Shows;
using FMFT.Web.Server.Services.Foundations.Users;
using FMFT.Web.Server.Services.Implementations.Cookies;
using FMFT.Web.Server.Services.Orchestrations.AccountReservations;
using FMFT.Web.Server.Services.Orchestrations.AccountShows;
using FMFT.Web.Server.Services.Orchestrations.UserAccounts;
using FMFT.Web.Server.Services.Processings.Accounts;
using FMFT.Web.Server.Services.Processings.Reservations;
using FMFT.Web.Server.Services.Processings.Shows;
using FMFT.Web.Server.Services.Processings.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace FMFT.Web.Server.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddFMFTOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JWTAuthenticationOptions>(configuration.GetSection(JWTAuthenticationOptions.SectionKey));
            services.Configure<GoogleAuthenticationOptions>(configuration.GetSection(GoogleAuthenticationOptions.SectionKey));
            services.Configure<FacebookAuthenticationOptions>(configuration.GetSection(FacebookAuthenticationOptions.SectionKey));

            return services;
        }

        public static IServiceCollection AddFMFTAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            JWTAuthenticationOptions jwtOptions = JWTAuthenticationOptions.FromConfiguration(configuration);
            GoogleAuthenticationOptions googleOptions = GoogleAuthenticationOptions.FromConfiguration(configuration);
            FacebookAuthenticationOptions facebookOptions = FacebookAuthenticationOptions.FromConfiguration(configuration);

            AuthenticationBuilder authenticationBuilder = services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = jwtOptions.Issuer,
                        ValidAudience = jwtOptions.Audience,
                        IssuerSigningKey = jwtOptions.SymmetricSecurityKey,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = true
                    };
                    options.EventsType = typeof(CustomCookieAuthenticationEvents);
                })
                .AddCookie(FMFTAuthenticationDefaults.ExternalScheme, o =>
                {
                    o.Cookie.Name = FMFTAuthenticationDefaults.ExternalScheme;
                    o.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                });
            
            if (googleOptions.Enabled)
            {
                authenticationBuilder.AddGoogle(options =>
                {
                    options.ClientId = googleOptions.ClientId;
                    options.ClientSecret = googleOptions.ClientSecret;
                });
            }

            if (facebookOptions.Enabled)
            {
                authenticationBuilder.AddFacebook(options =>
                {
                    options.AppId = facebookOptions.AppId;
                    options.AppSecret = facebookOptions.AppSecret;
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
            services.AddScoped<ILoggingBroker, LoggingBroker>();
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
            services.AddTransient<IAccountReservationOrchestrationService, AccountReservationOrchestrationService>();
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

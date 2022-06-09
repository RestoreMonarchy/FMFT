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

namespace FMFT.Web.Server.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddBrokers(this IServiceCollection services)
        {
            services.AddScoped<IStorageBroker, StorageBroker>();
            services.AddScoped<IAuthenticationBroker, AuthenticationBroker>();
            services.AddScoped<IEncryptionBroker, EncryptionBroker>();
            services.AddScoped<IValidationBroker, ValidationBroker>();
            return services;
        }

        public static IServiceCollection AddFoundations(this IServiceCollection services)
        {
            services.AddTransient<ISeatService, SeatService>();
            services.AddTransient<IAuditoriumService, AuditoriumService>();
            services.AddTransient<IShowService, ShowService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IReservationService, ReservationService>();
            return services;
        }

        public static IServiceCollection AddProcessings(this IServiceCollection services)
        {
            services.AddTransient<IUserProcessingService, UserProcessingService>();
            return services;
        }

        public static IServiceCollection AddImplementations(this IServiceCollection services)
        {
            services.AddTransient<CustomCookieAuthenticationEvents>();
            return services;
        }
    }
}

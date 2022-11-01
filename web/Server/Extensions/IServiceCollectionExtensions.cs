﻿using FMFT.Emails.Server.Extensions;
using FMFT.Web.Server.Brokers.Authentications;
using FMFT.Web.Server.Brokers.Emails;
using FMFT.Web.Server.Brokers.Encryptions;
using FMFT.Web.Server.Brokers.Loggings;
using FMFT.Web.Server.Brokers.Storages;
using FMFT.Web.Server.Brokers.Urls;
using FMFT.Web.Server.Brokers.Validations;
using FMFT.Web.Server.Models.Options;
using FMFT.Web.Server.Models.Options.Authentications;
using FMFT.Web.Server.Models.Options.Emails;
using FMFT.Web.Server.Services.Coordinations.Reservations;
using FMFT.Web.Server.Services.Foundations.Accounts;
using FMFT.Web.Server.Services.Foundations.Auditoriums;
using FMFT.Web.Server.Services.Foundations.Emails;
using FMFT.Web.Server.Services.Foundations.Reservations;
using FMFT.Web.Server.Services.Foundations.ResetPasswordRequests;
using FMFT.Web.Server.Services.Foundations.Seats;
using FMFT.Web.Server.Services.Foundations.Shows;
using FMFT.Web.Server.Services.Foundations.Users;
using FMFT.Web.Server.Services.Orchestrations.Reservations;
using FMFT.Web.Server.Services.Orchestrations.ResetPasswordRequests;
using FMFT.Web.Server.Services.Orchestrations.Shows;
using FMFT.Web.Server.Services.Orchestrations.UserAccounts;

namespace FMFT.Web.Server.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddServerEmailGenerator();

            return services;

        }

        public static IServiceCollection AddFMFTOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ServicesOptions>(configuration.GetSection(ServicesOptions.SectionKey));

            services.Configure<JWTAuthenticationOptions>(configuration.GetSection(JWTAuthenticationOptions.SectionKey));
            //services.Configure<GoogleAuthenticationOptions>(configuration.GetSection(GoogleAuthenticationOptions.SectionKey));
            //services.Configure<FacebookAuthenticationOptions>(configuration.GetSection(FacebookAuthenticationOptions.SectionKey));

            services.Configure<SmtpEmailOptions>(configuration.GetSection(SmtpEmailOptions.SectionKey));

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
            services.AddScoped<IEmailBroker, EmailBroker>();

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
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IResetPasswordRequestService, ResetPasswordRequestService>();

            return services;
        }

        public static IServiceCollection AddOrchestrations(this IServiceCollection services)
        {
            services.AddTransient<IReservationOrchestrationService, ReservationOrchestrationService>();
            services.AddTransient<IUserAccountOrchestrationService, UserAccountOrchestrationService>();
            services.AddTransient<IShowOrchestrationService, ShowOrchestrationService>();
            services.AddTransient<IResetPasswordRequestOrchestrationService, ResetPasswordRequestOrchestrationService>();

            return services;
        }

        public static IServiceCollection AddCoordinations(this IServiceCollection services)
        {
            services.AddTransient<IReservationCoordinationService, ReservationCoordinationService>();

            return services;
        }
    }
}

﻿using Blazored.LocalStorage;
using BlazorPanzoom;
using FMFT.Extensions.Blazor.Facebook.Extensions;
using FMFT.Extensions.Blazor.Google.Extensions;
using FMFT.Web.Client.Brokers.APIs;
using FMFT.Web.Client.Brokers.ExternalLogins;
using FMFT.Web.Client.Brokers.JSRuntimes;
using FMFT.Web.Client.Brokers.Loggings;
using FMFT.Web.Client.Brokers.MemoryStorages;
using FMFT.Web.Client.Brokers.Navigations;
using FMFT.Web.Client.Brokers.Storages;
using FMFT.Web.Client.Services.Accounts;
using FMFT.Web.Client.Services.Medias;
using FMFT.Web.Client.Services.Pages;
using FMFT.Web.Client.StateContainers.UserAccounts;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace FMFT.Web.Client.Extensions
{
    public static class WebAssemblyHostBuilderExtensions
    {
        public static void AddDependencies(this WebAssemblyHostBuilder builder)
        {
            if (builder.HostEnvironment.IsDevelopment())
            {
                builder.Logging.SetMinimumLevel(LogLevel.Debug);
                builder.Logging.AddFilter("Microsoft.AspNetCore", LogLevel.Warning);
            }
            else
            {
                builder.Logging.SetMinimumLevel(LogLevel.Information);
            }

            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddBlazorPanzoomServices();
        }

        public static void AddExtensions(this WebAssemblyHostBuilder builder)
        {
            builder.Services.AddFacebook(options => 
            {
                options.AppId = builder.Configuration["FacebookAppId"];
                options.OnLogin = async (services, result) =>
                {
                    await services.GetRequiredService<IAccountService>().HandleFacebookLoginAsync(result);
                };
            });

            builder.Services.AddGoogle(options =>
            {
                options.ClientId = builder.Configuration["GoogleClientId"];
                options.OnLogin = async (services, result) =>
                {
                    await services.GetRequiredService<IAccountService>().HandleGoogleLoginAsync(result);
                };
            });
        }

        public static void AddStateContainers(this WebAssemblyHostBuilder builder)
        {
            builder.Services.AddSingleton<IUserAccountStateContainer, UserAccountStateContainer>();
        }

        public static void AddBrokers(this WebAssemblyHostBuilder builder)
        {
            builder.Services.AddScoped<INavigationBroker, NavigationBroker>();
            builder.Services.AddScoped<IAPIBroker, APIBroker>();
            builder.Services.AddScoped<IJSRuntimeBroker, JSRuntimeBroker>();
            builder.Services.AddScoped<ILoggingBroker, LoggingBroker>();
            builder.Services.AddScoped<IMemoryStorageBroker, MemoryStorageBroker>();
            builder.Services.AddScoped<IStorageBroker, StorageBroker>();
            builder.Services.AddScoped<IExternalLoginBroker, ExternalLoginBroker>();
        }

        public static void AddServices(this WebAssemblyHostBuilder builder)
        {
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IMediaService, MediaService>();

            builder.Services.AddScoped<OrderingPageService>();
        }
    }
}

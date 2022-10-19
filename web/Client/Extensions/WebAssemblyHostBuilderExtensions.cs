using Blazored.LocalStorage;
using FMFT.Web.Client.Brokers.APIs;
using FMFT.Web.Client.Brokers.JSRuntimes;
using FMFT.Web.Client.Brokers.Loggings;
using FMFT.Web.Client.Brokers.MemoryStorages;
using FMFT.Web.Client.Brokers.Navigations;
using FMFT.Web.Client.Brokers.Storages;
using FMFT.Web.Client.Services.Accounts;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Logging;

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
        }

        public static void AddBrokers(this WebAssemblyHostBuilder builder)
        {
            builder.Services.AddScoped<INavigationBroker, NavigationBroker>();
            builder.Services.AddScoped<IAPIBroker, APIBroker>();
            builder.Services.AddScoped<IJSRuntimeBroker, JSRuntimeBroker>();
            builder.Services.AddScoped<ILoggingBroker, LoggingBroker>();
            builder.Services.AddScoped<IMemoryStorageBroker, MemoryStorageBroker>();
            builder.Services.AddScoped<IStorageBroker, StorageBroker>();
        }

        public static void AddServices(this WebAssemblyHostBuilder builder)
        {
            builder.Services.AddScoped<IAccountService, AccountService>();
        }
    }
}

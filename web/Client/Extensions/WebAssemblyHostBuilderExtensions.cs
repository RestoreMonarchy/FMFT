using FMFT.Web.Client.Brokers.APIs;
using FMFT.Web.Client.Brokers.JSRuntimes;
using FMFT.Web.Client.Brokers.Loggings;
using FMFT.Web.Client.Brokers.MemoryStorages;
using FMFT.Web.Client.Brokers.Navigations;
using FMFT.Web.Client.Brokers.Storages;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace FMFT.Web.Client.Extensions
{
    public static class WebAssemblyHostBuilderExtensions
    {
        public static void AddBrokers(this WebAssemblyHostBuilder builder)
        {
            builder.Services.AddScoped<INavigationBroker, NavigationBroker>();
            builder.Services.AddScoped<IAPIBroker, APIBroker>();
            builder.Services.AddScoped<IJSRuntimeBroker, JSRuntimeBroker>();
            builder.Services.AddScoped<ILoggingBroker, LoggingBroker>();
            builder.Services.AddScoped<IMemoryStorageBroker, MemoryStorageBroker>();
            builder.Services.AddScoped<IStorageBroker, StorageBroker>();
        }
    }
}

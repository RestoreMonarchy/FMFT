using FMFT.Web.Client;
using FMFT.Web.Client.Brokers.APIs;
using FMFT.Web.Client.Brokers.JSRuntimes;
using FMFT.Web.Client.Brokers.Navigations;
using FMFT.Web.Client.Services.Foundations.Accounts;
using FMFT.Web.Client.Services.Foundations.Auditoriums;
using FMFT.Web.Client.Services.Foundations.Shows;
using FMFT.Web.Client.Services.Processings.Accounts;
using FMFT.Web.Client.Services.Views.Accounts;
using FMFT.Web.Client.Services.Views.Shows;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<INavigationBroker, NavigationBroker>();
builder.Services.AddScoped<IJSRuntimeBroker, JSRuntimeBroker>();
builder.Services.AddScoped<IAPIBroker, APIBroker>();

builder.Services.AddScoped<IShowService, ShowService>();
builder.Services.AddScoped<IAuditoriumService, AuditoriumService>();
builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddScoped<IAccountProcessingService, AccountProcessingService>();

builder.Services.AddScoped<IShowViewService, ShowViewService>();
builder.Services.AddScoped<IAccountViewService, AccountViewService>();

WebAssemblyHost host = builder.Build();

await host.Services.GetRequiredService<IAccountProcessingService>().InitializeAsync();

await host.RunAsync();



using Blazored.LocalStorage;
using FMFT.Web.Client;
using FMFT.Web.Client.Brokers.APIs;
using FMFT.Web.Client.Brokers.JSRuntimes;
using FMFT.Web.Client.Brokers.Localizations;
using FMFT.Web.Client.Brokers.Loggings;
using FMFT.Web.Client.Brokers.MemoryStorages;
using FMFT.Web.Client.Brokers.Navigations;
using FMFT.Web.Client.Brokers.Storages;
using FMFT.Web.Client.Services.Coordinations.UserAccountCultures;
using FMFT.Web.Client.Services.Foundations.Accounts;
using FMFT.Web.Client.Services.Foundations.AccountStores;
using FMFT.Web.Client.Services.Foundations.Auditoriums;
using FMFT.Web.Client.Services.Foundations.Cultures;
using FMFT.Web.Client.Services.Foundations.Reservations;
using FMFT.Web.Client.Services.Foundations.Shows;
using FMFT.Web.Client.Services.Foundations.Users;
using FMFT.Web.Client.Services.Orchestrations.AccountReservations;
using FMFT.Web.Client.Services.Orchestrations.Accounts;
using FMFT.Web.Client.Services.Orchestrations.Cultures;
using FMFT.Web.Client.Services.Orchestrations.UserAccounts;
using FMFT.Web.Client.Services.Processings.Accounts;
using FMFT.Web.Client.Services.Processings.AccountStores;
using FMFT.Web.Client.Services.Processings.Cultures;
using FMFT.Web.Client.Services.Processings.Reservations;
using FMFT.Web.Client.Services.Processings.Users;
using FMFT.Web.Client.Services.Views.AccountReservations;
using FMFT.Web.Client.Services.Views.Accounts;
using FMFT.Web.Client.Services.Views.Reservations;
using FMFT.Web.Client.Services.Views.Shows;
using FMFT.Web.Client.Services.Views.UserAccountCultures;
using FMFT.Web.Client.Services.Views.Users;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<INavigationBroker, NavigationBroker>();
builder.Services.AddScoped<IJSRuntimeBroker, JSRuntimeBroker>();
builder.Services.AddScoped<IAPIBroker, APIBroker>();
builder.Services.AddScoped<IStorageBroker, StorageBroker>();
builder.Services.AddScoped<ILocalizationBroker, LocalizationBroker>();
builder.Services.AddScoped<IMemoryStorageBroker, MemoryStorageBroker>();
builder.Services.AddScoped<ILoggingBroker, LoggingBroker>();

builder.Services.AddScoped<IShowService, ShowService>();
builder.Services.AddScoped<IAuditoriumService, AuditoriumService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICultureService, CultureService>();
builder.Services.AddScoped<IAccountStoreService, AccountStoreService>();

builder.Services.AddScoped<IAccountProcessingService, AccountProcessingService>();
builder.Services.AddScoped<IReservationProcessingService, ReservationProcessingService>();
builder.Services.AddScoped<IUserProcessingService, UserProcessingService>();
builder.Services.AddScoped<ICultureProcessingService, CultureProcessingService>();
builder.Services.AddScoped<IAccountStoreProcessingService, AccountStoreProcessingService>();

builder.Services.AddScoped<IAccountReservationOrchestrationService, AccountReservationOrchestrationService>();
builder.Services.AddScoped<IUserAccountOrchestrationService, UserAccountOrchestrationService>();
builder.Services.AddScoped<ICultureOrchestrationService, CultureOrchestrationService>();
builder.Services.AddScoped<IAccountOrchestrationService, AccountOrchestrationService>();

builder.Services.AddScoped<IUserAccountCultureCoordinationService, UserAccountCultureCoordinationService>();

builder.Services.AddScoped<IShowViewService, ShowViewService>();
builder.Services.AddScoped<IAccountViewService, AccountViewService>();
builder.Services.AddScoped<IAccountReservationViewService, AccountReservationViewService>();
builder.Services.AddScoped<IUserViewService, UserViewService>();
builder.Services.AddScoped<IReservationViewService, ReservationViewService>();
builder.Services.AddScoped<IUserAccountCultureViewService, UserAccountCultureViewService>();

WebAssemblyHost host = builder.Build();

BeforeStartup beforeStartup = new(host);
await beforeStartup.ExecuteAsync();

await host.RunAsync();
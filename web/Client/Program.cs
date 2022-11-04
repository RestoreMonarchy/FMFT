using FMFT.Web.Client;
using FMFT.Web.Client.Brokers.Loggings;
using FMFT.Web.Client.Extensions;
using FMFT.Web.Client.Services.Accounts;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Globalization;

WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.AddDependencies();
builder.AddExtensions();
builder.AddStateContainers();
builder.AddBrokers();
builder.AddServices();

CultureInfo.CurrentCulture = new CultureInfo("pl-PL");

Console.WriteLine($"Application environment: {builder.HostEnvironment.Environment}");

WebAssemblyHost host = builder.Build();

IAccountService accountService = host.Services.GetRequiredService<IAccountService>();

await accountService.InitializeAsync();

await host.RunAsync();

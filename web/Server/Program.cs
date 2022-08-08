using FMFT.Extensions.Authentication.Extensions;
using FMFT.Extensions.Authentication.Constants;
using FMFT.Web.Server.Extensions;
using FMFT.Web.Server.Services.Implementations.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

IConfiguration configuration = builder.Configuration;

builder.Services.AddBrokers();
builder.Services.AddFoundations();
builder.Services.AddProcessings();
builder.Services.AddOrchestrations();

builder.Services.AddImplementations();

builder.Services.AddHttpContextAccessor();

builder.Services.AddFMFTAuthentication(configuration);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseForwardedHeaders();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.MapControllers();

app.MapFallbackToFile("index.html");

app.Run();
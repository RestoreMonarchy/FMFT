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

builder.Services.AddImplementations();

builder.Services.AddHttpContextAccessor();

builder.Services.AddDefaultAuthentication()
    .AddCookie(FMFTAuthenticationDefaults.ApplicationScheme, o =>
    {
        o.Cookie.Name = FMFTAuthenticationDefaults.ApplicationScheme;
        o.ExpireTimeSpan = TimeSpan.FromHours(24);
        o.EventsType = typeof(CustomCookieAuthenticationEvents);
    })
    .AddCookie(FMFTAuthenticationDefaults.ExternalScheme, o =>
    {
        o.Cookie.Name = FMFTAuthenticationDefaults.ExternalScheme;
        o.ExpireTimeSpan = TimeSpan.FromMinutes(10);
    })
    .AddGoogle(options =>
    {
        options.ClientId = configuration.GetSection("Authentication").GetSection("Google")["ClientId"];
        options.ClientSecret = configuration.GetSection("Authentication").GetSection("Google")["ClientSecret"];
    })
    .AddFacebook(options => 
    {
        options.AppId = configuration.GetSection("Authentication").GetSection("Facebook")["AppId"];
        options.AppSecret = configuration.GetSection("Authentication").GetSection("Facebook")["AppSecret"];
    });

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

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
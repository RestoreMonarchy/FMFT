using FMFT.Extensions.Authentication.Server.Extensions;
using FMFT.Extensions.Authentication.Shared.Constants;
using FMFT.Web.Server.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddBrokers();
builder.Services.AddFoundations();
builder.Services.AddProcessings();

builder.Services.AddHttpContextAccessor();

builder.Services.AddDefaultAuthentication()
    .AddCookie(FMFTAuthenticationDefaults.ApplicationScheme, o =>
    {
        o.ExpireTimeSpan = TimeSpan.FromHours(24);
    })
    .AddCookie(FMFTAuthenticationDefaults.ExternalScheme, o =>
    {
        o.Cookie.Name = FMFTAuthenticationDefaults.ExternalScheme;
        o.ExpireTimeSpan = TimeSpan.FromMinutes(10);
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
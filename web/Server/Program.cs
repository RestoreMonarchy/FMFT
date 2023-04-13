using AspNetCoreRateLimit;
using FMFT.Web.Server.Extensions;
using Hangfire;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilogLogging();

// Add services to the container.

IConfiguration configuration = builder.Configuration;

builder.Services.AddDependencies(configuration);
builder.Services.AddBrokers();
builder.Services.AddFoundations();
builder.Services.AddOrchestrations();
builder.Services.AddCoordinations();

builder.Services.AddFMFTOptions(configuration);

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddSwaggerGen(options => 
    {
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "Please insert JWT with Bearer into field",
            In = ParameterLocation.Header,
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey
        });

        OpenApiSecurityScheme securityScheme = new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        };

        OpenApiSecurityRequirement securityRequirement = new()
        {
            { securityScheme, Array.Empty<string>() }
        };

        options.AddSecurityRequirement(securityRequirement);
    });    
}

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin();
        policy.AllowAnyMethod();
        policy.AllowAnyHeader();
    });
});

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.All;
    options.ForwardedHeaders = ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedFor;
    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
});

var app = builder.Build();

app.UseIpRateLimiting();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHangfireDashboard();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseForwardedHeaders();

app.UseCors();

app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();

app.MapControllers();

app.Run();
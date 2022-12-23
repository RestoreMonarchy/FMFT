using Serilog;

namespace FMFT.Web.Server.Extensions
{
    public static class IHostBuilderExtensions
    {
        public static IHostBuilder UseSerilogLogging(this IHostBuilder builder)
        {
            builder.UseSerilog((hostingContext, loggerConfiguration) => 
            { 
                loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration);
            });

            return builder;
        }
    }
}

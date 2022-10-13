using FMFT.Web.Client.Brokers.Loggings;
using FMFT.Web.Client.Models.Accounts.Exceptions;
using FMFT.Web.Client.Services.Orchestrations.Accounts;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace FMFT.Web.Client
{
    public class BeforeStartup
    {
        private readonly WebAssemblyHost host;

        public BeforeStartup(WebAssemblyHost host)
        {
            this.host = host;
        }

        private IConfiguration Configuration => host.Configuration;
        private IServiceProvider Services => host.Services;

        // This method works as an exposer
        public async ValueTask ExecuteAsync()
        {
            ILoggingBroker loggingBroker = Services.GetRequiredService<ILoggingBroker>();

            Configuration["AccountToken"] = "account_token";

            IAccountOrchestrationService accountOrchestrationService = Services.GetRequiredService<IAccountOrchestrationService>();
            
            try
            {
                await accountOrchestrationService.UpdateAccountStoreAsync();
            } catch (AccountNotAuthenticatedException)
            {
                loggingBroker.LogInformation("Not authenticated on the web API");
            }            
        }
    }
}

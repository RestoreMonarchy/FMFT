using FMFT.Web.Client.Services.Processings.Accounts;
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

        public async ValueTask ExecuteAsync()
        {
            IAccountProcessingService accountService = Services.GetRequiredService<IAccountProcessingService>();
            await accountService.UpdateAccountAsync();
        }
    }
}

using FMFT.Extensions.Blazor.Facebook.Services;

namespace FMFT.Web.Client.Brokers.ExternalLogins
{
    public class ExternalLoginBroker : IExternalLoginBroker
    {
        private readonly FacebookService facebookService;

        public ExternalLoginBroker(FacebookService facebookService)
        {
            this.facebookService = facebookService;
        }

        public async ValueTask InitializeFacebookAsync()
        {
            await facebookService.InitializeAsync();
        }

        public async ValueTask LoginFacebookAsync()
        {
            await facebookService.LoginAsync();
        }
    }
}

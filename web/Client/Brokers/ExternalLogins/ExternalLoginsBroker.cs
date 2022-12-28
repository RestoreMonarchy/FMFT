using FMFT.Extensions.Blazor.Facebook.Services;
using FMFT.Extensions.Blazor.Google.Services;

namespace FMFT.Web.Client.Brokers.ExternalLogins
{
    public class ExternalLoginBroker : IExternalLoginBroker
    {
        private readonly FacebookService facebookService;
        private readonly GoogleService googleService;

        public ExternalLoginBroker(FacebookService facebookService, GoogleService googleService)
        {
            this.facebookService = facebookService;
            this.googleService = googleService;
        }

        public async ValueTask InitializeFacebookAsync()
        {
            await facebookService.InitializeAsync();
        }

        public async ValueTask LoginFacebookAsync()
        {
            await facebookService.LoginAsync();
        }

        public async ValueTask InitializeGoogleAsync()
        {
            await googleService.InitializeAsync();
        }

        public async ValueTask LoginGoogleAsync()
        {
            await googleService.LoginAsync();
        }
    }
}

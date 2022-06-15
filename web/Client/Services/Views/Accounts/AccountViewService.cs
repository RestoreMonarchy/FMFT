using FMFT.Web.Client.Brokers.Navigations;
using FMFT.Web.Client.Services.Processings.Accounts;
using FMFT.Web.Shared.Models.Users;
using FMFT.Web.Shared.Models.Users.Models;

namespace FMFT.Web.Client.Services.Views.Accounts
{
    public class AccountViewService : IAccountViewService
    {
        private readonly IAccountProcessingService accountService;
        private readonly INavigationBroker navigationBroker;

        public AccountViewService(IAccountProcessingService accountService, INavigationBroker navigationBroker)
        {
            this.accountService = accountService;
            this.navigationBroker = navigationBroker;
        }

        public UserInfo UserInfo 
            => accountService.UserInfo;

        public async ValueTask LoginAsync(SignInUserWithPasswordModel model) 
            => await accountService.LoginAsync(model);

        public async ValueTask RegisterAsync(RegisterUserWithPasswordModel model)
            => await accountService.RegisterAsync(model);

        public void ForceLoadNavigateTo(string url)
            => navigationBroker.ForceLoadNavigateTo(url);
    }
}

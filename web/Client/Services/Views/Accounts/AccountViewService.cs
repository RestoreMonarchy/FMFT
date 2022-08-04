using FMFT.Web.Client.Brokers.Navigations;
using FMFT.Web.Client.Models.Accounts;
using FMFT.Web.Client.Models.Accounts.Requests;
using FMFT.Web.Client.Services.Processings.Accounts;

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

        public Account Account 
            => accountService.Account;

        public async ValueTask LoginAsync(LogInWithPasswordRequest request) 
            => await accountService.LoginAsync(request);

        public async ValueTask RegisterAsync(RegisterWithPasswordRequest request)
            => await accountService.RegisterAsync(request);

        public async ValueTask ConfirmExternalLoginAsync(ConfirmExternalLoginRequest request)
            => await accountService.ConfirmExternalLoginAsync(request);

        public void ForceLoadNavigateTo(string url)
            => navigationBroker.ForceLoadNavigateTo(url);
    }
}

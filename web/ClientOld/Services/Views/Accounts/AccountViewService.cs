﻿using FMFT.Web.Client.Brokers.Navigations;
using FMFT.Web.Client.Models.Accounts;
using FMFT.Web.Client.Models.Accounts.Requests;
using FMFT.Web.Client.Services.Orchestrations.Accounts;
using FMFT.Web.Client.Services.Processings.Accounts;

namespace FMFT.Web.Client.Services.Views.Accounts
{
    public class AccountViewService : IAccountViewService
    {
        private readonly IAccountOrchestrationService accountService;
        private readonly INavigationBroker navigationBroker;

        public AccountViewService(IAccountOrchestrationService accountService, INavigationBroker navigationBroker)
        {
            this.accountService = accountService;
            this.navigationBroker = navigationBroker;
        }

        public UserAccount Account 
            => accountService.RetrieveAccountStore();

        public async ValueTask LoginAsync(LogInWithPasswordRequest request) 
            => await accountService.LoginAsync(request);

        public async ValueTask RegisterAsync(RegisterWithPasswordRequest request)
            => await accountService.RegisterAsync(request);

        public void ForceLoadNavigateTo(string url)
            => navigationBroker.ForceLoadNavigateTo(url);
    }
}
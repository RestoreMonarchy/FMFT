using FMFT.Web.Client.Models.Accounts;
using FMFT.Web.Client.Models.Accounts.Requests;
using FMFT.Web.Client.Services.Processings.Accounts;
using FMFT.Web.Client.Services.Processings.AccountStores;

namespace FMFT.Web.Client.Services.Orchestrations.Accounts
{
    public class AccountOrchestrationService : IAccountOrchestrationService
    {
        private readonly IAccountProcessingService accountService;
        private readonly IAccountStoreProcessingService accountStoreService;

        public AccountOrchestrationService(IAccountProcessingService accountService, IAccountStoreProcessingService accountStoreService)
        {
            this.accountService = accountService;
            this.accountStoreService = accountStoreService;
        }

        public Account RetrieveAccountStore()
        {
            return accountStoreService.RetrieveAccount();
        }

        public async ValueTask LoginAsync(LogInWithPasswordRequest request)
            => await accountService.LoginAsync(request);

        public async ValueTask RegisterAsync(RegisterWithPasswordRequest request)
            => await accountService.RegisterAsync(request);

        public async ValueTask ConfirmExternalLoginAsync(ConfirmExternalLoginRequest request)
            => await accountService.ConfirmExternalLoginAsync(request);

        public async ValueTask UpdateAccountStoreAsync()
        {
            Account account = await accountService.RetrieveAccountAsync();
            accountStoreService.UpdateAccount(account);
        }
    }
}

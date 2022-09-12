using FMFT.Web.Client.Models.Accounts;
using FMFT.Web.Client.Services.Foundations.AccountStores;

namespace FMFT.Web.Client.Services.Processings.AccountStores
{
    public class AccountStoreProcessingService : IAccountStoreProcessingService
    {
        private readonly IAccountStoreService accountStoreService;

        public AccountStoreProcessingService(IAccountStoreService accountStoreService)
        {
            this.accountStoreService = accountStoreService;
        }

        public Account RetrieveAccount()
        {
            return accountStoreService.RetrieveAccount();
        }

        public void UpdateAccount(Account account)
        {
            accountStoreService.UpdateAccount(account);
        }
    }
}

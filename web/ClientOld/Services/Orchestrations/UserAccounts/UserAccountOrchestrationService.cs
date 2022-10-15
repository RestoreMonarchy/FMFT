using FMFT.Web.Client.Models.Accounts;
using FMFT.Web.Client.Models.Accounts.Exceptions;
using FMFT.Web.Client.Services.Processings.Accounts;
using FMFT.Web.Client.Services.Processings.AccountStores;
using FMFT.Web.Client.Services.Processings.Users;
using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Client.Services.Orchestrations.UserAccounts
{
    public class UserAccountOrchestrationService : IUserAccountOrchestrationService
    {
        private readonly IUserProcessingService userService;
        private readonly IAccountStoreProcessingService accountStoreService;

        public UserAccountOrchestrationService(IUserProcessingService userService, IAccountStoreProcessingService accountStoreService)
        {
            this.userService = userService;
            this.accountStoreService = accountStoreService;
        }

        public UserAccount RetrieveAccountStore()
        {
            return accountStoreService.RetrieveAccount();
        }

        public async ValueTask UpdateAccountCultureAsync(CultureId cultureId)
        {
            UserAccount account = RetrieveAccountStore();
            if (account == null)
            {
                throw new AccountNotAuthenticatedException();
            }
            await userService.UpdateUserCultureAsync(account.UserId, cultureId);
        }
    }
}

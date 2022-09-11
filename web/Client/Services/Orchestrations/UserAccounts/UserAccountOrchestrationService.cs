using FMFT.Web.Client.Models.Accounts;
using FMFT.Web.Client.Services.Processings.Accounts;
using FMFT.Web.Client.Services.Processings.Users;
using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Client.Services.Orchestrations.UserAccounts
{
    public class UserAccountOrchestrationService : IUserAccountOrchestrationService
    {
        private readonly IUserProcessingService userService;
        private readonly IAccountProcessingService accountService;

        public UserAccountOrchestrationService(IUserProcessingService userService, IAccountProcessingService accountService)
        {
            this.userService = userService;
            this.accountService = accountService;
        }

        public Account RetrieveAccount()
        {
            return accountService.RetrieveAccount();
        }

        public async ValueTask UpdateAccountCultureAsync(CultureId cultureId)
        {
            Account account = accountService.RetrieveAccount();
            await userService.UpdateUserCultureAsync(account.UserId, cultureId);
        }
    }
}

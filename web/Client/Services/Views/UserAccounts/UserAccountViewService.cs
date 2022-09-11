using FMFT.Web.Client.Models.Accounts;
using FMFT.Web.Client.Services.Orchestrations.UserAccounts;
using FMFT.Web.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace FMFT.Web.Client.Services.Views.UserAccounts
{
    public class UserAccountViewService : IUserAccountViewService
    {
        private readonly IUserAccountOrchestrationService userAccountService;

        public UserAccountViewService(IUserAccountOrchestrationService userAccountService)
        {
            this.userAccountService = userAccountService;
        }

        public async ValueTask ChangeCultureAsync(CultureId cultureId)
            => await userAccountService.UpdateAccountCultureAsync(cultureId);

        public Account RetrieveAccount() 
            => userAccountService.RetrieveAccount();

    }
}

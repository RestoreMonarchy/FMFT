using FMFT.Web.Client.Models.Accounts;
using FMFT.Web.Client.Services.Orchestrations.Cultures;
using FMFT.Web.Client.Services.Orchestrations.UserAccounts;
using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Client.Services.Coordinations.UserAccountCultures
{
    public class UserAccountCultureCoordinationService : IUserAccountCultureCoordinationService
    {
        private readonly IUserAccountOrchestrationService userAccountService;
        private readonly ICultureOrchestrationService cultureService;

        public UserAccountCultureCoordinationService(IUserAccountOrchestrationService userAccountService, ICultureOrchestrationService cultureService)
        {
            this.userAccountService = userAccountService;
            this.cultureService = cultureService;
        }

        public async ValueTask SyncUserAccountCulturesAsync()
        {
            CultureId cultureId = await cultureService.RetrieveCultureIdAsync();
            Account account = userAccountService.RetrieveAccountStore();
            if (account != null && account.CultureId != cultureId)
            {
                await userAccountService.UpdateAccountCultureAsync(cultureId);
            }
        }

        public async ValueTask UpdateCultureAsync(CultureId cultureId)
        {
            await cultureService.UpdateCultureIdAsync(cultureId);

            Account account = RetrieveAccountStore();
            if (account != null)
            {
                await userAccountService.UpdateAccountCultureAsync(cultureId);
            }            
        }

        public Account RetrieveAccountStore()
        {
            return userAccountService.RetrieveAccountStore();
        }

        public async ValueTask<CultureId> RetrieveCultureIdAsync()
        {
            return await cultureService.RetrieveCultureIdAsync();
        }
    }
}

using FMFT.Web.Client.Models.Accounts;
using FMFT.Web.Client.Services.Coordinations.UserAccountCultures;
using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Client.Services.Views.UserAccountCultures
{
    public class UserAccountCultureViewService : IUserAccountCultureViewService
    {
        private readonly IUserAccountCultureCoordinationService userAccountCultureService;

        public UserAccountCultureViewService(IUserAccountCultureCoordinationService userAccountCultureService)
        {
            this.userAccountCultureService = userAccountCultureService;
        }

        public async ValueTask ChangeCultureAsync(CultureId cultureId)
            => await userAccountCultureService.UpdateCultureAsync(cultureId);

        public async ValueTask<CultureId> RetrieveCultureIdAsync()
            => await userAccountCultureService.RetrieveCultureIdAsync();

    }
}

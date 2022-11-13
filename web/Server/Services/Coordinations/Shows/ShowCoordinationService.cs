using FMFT.Web.Server.Models.Shows;
using FMFT.Web.Server.Models.Shows.Params;
using FMFT.Web.Server.Services.Orchestrations.Shows;
using FMFT.Web.Server.Services.Orchestrations.UserAccounts;
using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Server.Services.Coordinations.Shows
{
    public class ShowCoordinationService : IShowCoordinationService
    {
        private readonly IShowOrchestrationService showService;
        private readonly IUserAccountOrchestrationService userAccountService;

        public ShowCoordinationService(IShowOrchestrationService showService, IUserAccountOrchestrationService userAccountService)
        {
            this.showService = showService;
            this.userAccountService = userAccountService;
        }

        public async ValueTask<Show> AddShowAsync(AddShowParams @params)
        {
            await userAccountService.AuthorizeUserAccountByRoleAsync(UserRole.Admin);

            return await showService.AddShowAsync(@params);
        }

        public async ValueTask<Show> ModifyShowAsync(UpdateShowParams @params)
        {
            await userAccountService.AuthorizeUserAccountByRoleAsync(UserRole.Admin);

            return await showService.ModifyShowAsync(@params);
        }

        public async ValueTask<Show> RetrieveShowByIdAsync(int showId)
        {
            return await showService.RetrieveShowByIdAsync(showId);
        }

        public async ValueTask<IEnumerable<Show>> RetrieveAllShowsAsync()
        {
            return await showService.RetrieveAllShowsAsync();
        }
    }
}

using FMFT.Web.Server.Models.Shows;
using FMFT.Web.Server.Models.Shows.Params;
using FMFT.Web.Server.Services.Processings.Accounts;
using FMFT.Web.Server.Services.Processings.Shows;
using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Server.Services.Orchestrations.AccountShows
{
    public class AccountShowOrchestrationService : IAccountShowOrchestrationService
    {
        private readonly IAccountProcessingService accountService;
        private readonly IShowProcessingService showService;

        public AccountShowOrchestrationService(IAccountProcessingService accountService, IShowProcessingService showService)
        {
            this.accountService = accountService;
            this.showService = showService;
        }

        public async ValueTask<Show> AddShowAsync(AddShowParams @params)
        {
            accountService.AuthorizeAccountByRole(UserRole.Admin);
            return await showService.AddShowAsync(@params);
        }

        public async ValueTask<Show> ModifyShowAsync(UpdateShowParams @params)
        {
            accountService.AuthorizeAccountByRole(UserRole.Admin);
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

using FMFT.Web.Server.Models.Shows;
using FMFT.Web.Server.Models.Shows.Params;
using FMFT.Web.Server.Services.Processings.Shows;

namespace FMFT.Web.Server.Services.Orchestrations.Shows
{
    public class ShowOrchestrationService : IShowOrchestrationService
    {
        private readonly IShowProcessingService showService;

        public ShowOrchestrationService(IShowProcessingService showService)
        {
            this.showService = showService;
        }

        public async ValueTask<Show> AddShowAsync(AddShowParams @params)
        {
            return await showService.AddShowAsync(@params);
        }

        public async ValueTask<Show> ModifyShowAsync(UpdateShowParams @params)
        {
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

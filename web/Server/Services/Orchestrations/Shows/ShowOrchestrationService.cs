using FMFT.Web.Server.Brokers.Loggings;
using FMFT.Web.Server.Models.Shows;
using FMFT.Web.Server.Models.Shows.Params;
using FMFT.Web.Server.Services.Foundations.Shows;

namespace FMFT.Web.Server.Services.Orchestrations.Shows
{
    public partial class ShowOrchestrationService : IShowOrchestrationService
    {
        private readonly IShowService showService;
        private readonly ILoggingBroker loggingBroker;

        public ShowOrchestrationService(IShowService showService, ILoggingBroker loggingBroker)
        {
            this.showService = showService;
            this.loggingBroker = loggingBroker;
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

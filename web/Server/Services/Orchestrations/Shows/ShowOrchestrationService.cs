using FMFT.Extensions.TheStandard;
using FMFT.Web.Server.Brokers.Loggings;
using FMFT.Web.Server.Models.Shows;
using FMFT.Web.Server.Models.Shows.Params;
using FMFT.Web.Server.Services.Processings.Shows;

namespace FMFT.Web.Server.Services.Orchestrations.Shows
{
    public partial class ShowOrchestrationService : TheStandardService, IShowOrchestrationService
    {
        private readonly IShowProcessingService showService;
        private readonly ILoggingBroker loggingBroker;

        public ShowOrchestrationService(IShowProcessingService showService, ILoggingBroker loggingBroker)
        {
            this.showService = showService;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<Show> AddShowAsync(AddShowParams @params)
            => TryCatch(async () => 
            {
                return await showService.AddShowAsync(@params);
            });

        public ValueTask<Show> ModifyShowAsync(UpdateShowParams @params)
            => TryCatch(async () =>
            {
                return await showService.ModifyShowAsync(@params);
            });

        public ValueTask<Show> RetrieveShowByIdAsync(int showId)
           => TryCatch(async () =>
           {
               return await showService.RetrieveShowByIdAsync(showId);
           });

        public ValueTask<IEnumerable<Show>> RetrieveAllShowsAsync()
            => TryCatch(async () =>
            {
                return await showService.RetrieveAllShowsAsync();
            });
    }
}

using FMFT.Web.Server.Brokers.Loggings;
using FMFT.Web.Server.Models.ShowProducts;
using FMFT.Web.Server.Models.ShowProducts.Params;
using FMFT.Web.Server.Models.Shows;
using FMFT.Web.Server.Models.Shows.Exceptions;
using FMFT.Web.Server.Models.Shows.Params;
using FMFT.Web.Server.Services.Foundations.ShowProducts;
using FMFT.Web.Server.Services.Foundations.Shows;

namespace FMFT.Web.Server.Services.Orchestrations.Shows
{
    public partial class ShowOrchestrationService : IShowOrchestrationService
    {
        private readonly IShowService showService;
        private readonly IShowProductService showProductService;
        private readonly ILoggingBroker loggingBroker;

        public ShowOrchestrationService(IShowService showService, IShowProductService showProductService, ILoggingBroker loggingBroker)
        {
            this.showService = showService;
            this.showProductService = showProductService;
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

        public async ValueTask<Show> ModifyShowSellingDetailsAsync(UpdateShowSellingDetailsParams @params)
        {
            return await showService.ModifyShowSellingDetailsAsync(@params);
        }

        public async ValueTask<Show> RetrieveShowByIdAsync(int showId)
        {
            return await showService.RetrieveShowByIdAsync(showId);
        }

        public async ValueTask<Show> RetrievePublicShowByIdAsync(int showId)
        {
            return await showService.RetrievePublicShowByIdAsync(showId);
        }

        public async ValueTask<IEnumerable<Show>> RetrievePublicShowsAsync()
        {
            return await showService.RetrievePublicShowsAsync();
        }

        public async ValueTask<IEnumerable<Show>> RetrieveAllShowsAsync()
        {
            return await showService.RetrieveAllShowsAsync();
        }

        public async ValueTask<IEnumerable<ShowProduct>> RetrieveShowProductsByShowIdAsync(int showId)
        {
            return await showProductService.RetrieveShowProductsByShowIdAsync(showId);
        }

        public async ValueTask<ShowProduct> AddShowProductAsync(AddShowProductParams @params)
        {
            return await showProductService.AddShowProductAsync(@params);
        }

        public async ValueTask<ShowProduct> ModifyShowProductAsync(UpdateShowProductParams @params)
        {
            return await showProductService.ModifyShowProductAsync(@params);
        }

        public async ValueTask RemoveShowProductByIdAndShowIdAsync(int showProductId, int showId)
        {
            await showProductService.RemoveShowProductByIdAndShowIdAsync(showProductId, showId);
        }
    }
}

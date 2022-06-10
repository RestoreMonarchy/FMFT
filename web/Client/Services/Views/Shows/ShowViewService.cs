using FMFT.Web.Client.Services.Foundations.Shows;
using FMFT.Web.Shared.Models.Shows;

namespace FMFT.Web.Client.Services.Views.Shows
{
    public class ShowViewService : IShowViewService
    {
        private readonly IShowService showService;

        public ShowViewService(IShowService showService)
        {
            this.showService = showService;
        }

        public async ValueTask<List<Show>> RetrieveAllShowsAsync()
        {
            return await showService.RetrieveAllShowsAsync();
        }

        public async ValueTask<Show> RetrieveShowByIdAsync(int showId)
        {
            return await showService.RetrieveShowByIdAsync(showId);
        }
    }
}

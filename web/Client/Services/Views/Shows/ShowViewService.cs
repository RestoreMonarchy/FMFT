using FMFT.Web.Client.Services.Foundations.Auditoriums;
using FMFT.Web.Client.Services.Foundations.Shows;
using FMFT.Web.Shared.Models.Auditoriums;
using FMFT.Web.Shared.Models.Shows;

namespace FMFT.Web.Client.Services.Views.Shows
{
    public class ShowViewService : IShowViewService
    {
        private readonly IShowService showService;
        private readonly IAuditoriumService auditoriumService;

        public ShowViewService(IShowService showService, IAuditoriumService auditoriumService)
        {
            this.showService = showService;
            this.auditoriumService = auditoriumService;
        }

        public async ValueTask<List<Show>> RetrieveAllShowsAsync()
        {
            return await showService.RetrieveAllShowsAsync();
        }

        public async ValueTask<Show> RetrieveShowByIdAsync(int showId)
        {
            return await showService.RetrieveShowByIdAsync(showId);
        }

        public async ValueTask<Auditorium> RetrieveAuditoriumAsync(int auditoriumId)
        {
            return await auditoriumService.RetrieveAuditoriumByIdAsync(auditoriumId);
        }
    }
}

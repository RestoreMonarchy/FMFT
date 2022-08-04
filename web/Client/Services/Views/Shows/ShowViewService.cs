using FMFT.Web.Client.Services.Foundations.Auditoriums;
using FMFT.Web.Client.Services.Foundations.Reservations;
using FMFT.Web.Client.Services.Foundations.Shows;
using FMFT.Web.Client.Services.Processings.Accounts;
using FMFT.Web.Server.Models.Auditoriums;
using FMFT.Web.Server.Models.Reservations;
using FMFT.Web.Server.Models.Reservations.Models;
using FMFT.Web.Server.Models.Shows;

namespace FMFT.Web.Client.Services.Views.Shows
{
    public class ShowViewService : IShowViewService
    {
        private readonly IShowService showService;
        private readonly IAuditoriumService auditoriumService;
        private readonly IReservationService reservationService;

        public ShowViewService(IShowService showService,
            IAuditoriumService auditoriumService,
            IReservationService reservationService)
        {
            this.showService = showService;
            this.auditoriumService = auditoriumService;
            this.reservationService = reservationService;
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

        public async ValueTask<Reservation> CreateReservationAsync(CreateReservationModel model)
        {
            return await reservationService.CreateReservationAsync(model);
        }
    }
}

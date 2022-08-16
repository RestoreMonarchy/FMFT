using FMFT.Web.Client.Models.AccountReservations.Arguments;
using FMFT.Web.Client.Models.Auditoriums;
using FMFT.Web.Client.Models.Reservations;
using FMFT.Web.Client.Models.Shows;
using FMFT.Web.Client.Models.Shows.Params;
using FMFT.Web.Client.Services.Foundations.Auditoriums;
using FMFT.Web.Client.Services.Foundations.Shows;
using FMFT.Web.Client.Services.Orchestrations.AccountReservations;

namespace FMFT.Web.Client.Services.Views.Shows
{
    public class ShowViewService : IShowViewService
    {
        private readonly IShowService showService;
        private readonly IAuditoriumService auditoriumService;
        private readonly IAccountReservationOrchestrationService accountReservationService;

        public ShowViewService(IShowService showService,
            IAuditoriumService auditoriumService,
            IAccountReservationOrchestrationService accountReservationService)
        {
            this.showService = showService;
            this.auditoriumService = auditoriumService;
            this.accountReservationService = accountReservationService;
        }

        public async ValueTask<List<Show>> RetrieveAllShowsAsync()
        {
            return await showService.RetrieveAllShowsAsync();
        }

        public async ValueTask<Show> RetrieveShowByIdAsync(int showId)
        {
            return await showService.RetrieveShowByIdAsync(showId);
        }

        public async ValueTask<Show> AddShowAsync(AddShowParams @params)
        {
            return await showService.AddShowAsync(@params);
        }

        public async ValueTask<Show> UpdateShowAsync(UpdateShowParams @params)
        {
            return await showService.UpdateShowAsync(@params);
        }

        public async ValueTask<List<Auditorium>> RetrieveAllAuditoriumsAsync()
        {
            return await auditoriumService.RetrieveAllAuditoriumsAsync();
        }

        public async ValueTask<Auditorium> RetrieveAuditoriumByIdAsync(int auditoriumId)
        {
            return await auditoriumService.RetrieveAuditoriumByIdAsync(auditoriumId);
        }

        public async ValueTask<Reservation> CreateAccountReservationAsync(CreateAccountReservationArguments arguments)
        {
            return await accountReservationService.CreateAccountReservationAsync(arguments);
        }
    }
}

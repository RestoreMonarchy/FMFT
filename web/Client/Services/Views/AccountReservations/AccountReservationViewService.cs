using FMFT.Web.Client.Models.Auditoriums;
using FMFT.Web.Client.Models.Reservations;
using FMFT.Web.Client.Services.Foundations.Auditoriums;
using FMFT.Web.Client.Services.Orchestrations.AccountReservations;
using FMFT.Web.Client.Services.Processings.Reservations;

namespace FMFT.Web.Client.Services.Views.AccountReservations
{
    public class AccountReservationViewService : IAccountReservationViewService
    {
        private readonly IAccountReservationOrchestrationService accountReservationService;
        private readonly IReservationProcessingService reservationService;
        private readonly IAuditoriumService auditoriumService;

        public AccountReservationViewService(
            IAccountReservationOrchestrationService accountReservationService, 
            IReservationProcessingService reservationService, 
            IAuditoriumService auditoriumService)
        {
            this.accountReservationService = accountReservationService;
            this.reservationService = reservationService;
            this.auditoriumService = auditoriumService;
        }

        public async ValueTask<List<Reservation>> RetrieveAccountReservationsAsync()
        {
            return await accountReservationService.RetrieveAccountReservationsAsync();
        }

        public async ValueTask<Reservation> RetrieveReservationByIdAsync(int reservationId)
        {
            return await reservationService.RetrieveReservationByIdAsync(reservationId);
        }

        public async ValueTask<Auditorium> RetrieveAuditoriumByIdAsync(int auditoriumId)
        {
            return await auditoriumService.RetrieveAuditoriumByIdAsync(auditoriumId);
        }
    }
}

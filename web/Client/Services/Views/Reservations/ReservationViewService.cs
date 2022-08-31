using FMFT.Web.Client.Models.Auditoriums;
using FMFT.Web.Client.Models.Reservations;
using FMFT.Web.Client.Services.Foundations.Auditoriums;
using FMFT.Web.Client.Services.Processings.Reservations;

namespace FMFT.Web.Client.Services.Views.Reservations
{
    public class ReservationViewService : IReservationViewService
    {
        private readonly IReservationProcessingService reservationService;
        private readonly IAuditoriumService auditoriumService;

        public ReservationViewService(IReservationProcessingService reservationService, IAuditoriumService auditoriumService)
        {
            this.reservationService = reservationService;
            this.auditoriumService = auditoriumService;
        }

        public async ValueTask<List<Reservation>> RetrieveAllReservationsAsync()
        {
            return await reservationService.RetrieveAllReservationsAsync();
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

using FMFT.Web.Client.Models.Reservations;
using FMFT.Web.Client.Services.Processings.Reservations;

namespace FMFT.Web.Client.Services.Views.Reservations
{
    public class ReservationViewService : IReservationViewService
    {
        private readonly IReservationProcessingService reservationService;

        public ReservationViewService(IReservationProcessingService reservationService)
        {
            this.reservationService = reservationService;
        }

        public async ValueTask<List<Reservation>> RetrieveAllReservationsAsync()
        {
            return await reservationService.RetrieveAllReservationsAsync();
        }
    }
}

using FMFT.Web.Client.Models.Reservations;
using FMFT.Web.Client.Models.Reservations.Requests;
using FMFT.Web.Client.Services.Foundations.Reservations;

namespace FMFT.Web.Client.Services.Processings.Reservations
{
    public class ReservationProcessingService : IReservationProcessingService
    {
        private readonly IReservationService reservationService;

        public ReservationProcessingService(IReservationService reservationService)
        {
            this.reservationService = reservationService;
        }

        public async ValueTask<Reservation> CreateReservationAsync(CreateReservationRequest request)
        {
            return await reservationService.CreateReservationAsync(request);
        }

        public async ValueTask<List<Reservation>> RetrieveReservationsByUserIdAsync(int userId)
        {
            return await reservationService.RetrieveReservationsByUserIdAsync(userId);
        }

        public async ValueTask<Reservation> RetrieveReservationByIdAsync(int reservationId)
        {
            return await reservationService.RetrieveReservationByIdAsync(reservationId);
        }

        public async ValueTask<List<Reservation>> RetrieveAllReservationsAsync()
        {
            return await reservationService.RetrieveAllReservationsAsync();
        }
    }
}

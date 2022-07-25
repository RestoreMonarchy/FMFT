using FMFT.Web.Client.Services.Foundations.Reservations;
using FMFT.Web.Shared.Models.Reservations;
using FMFT.Web.Shared.Models.Reservations.Models;

namespace FMFT.Web.Client.Services.Processings.Reservations
{
    public class ReservationProcessingService : IReservationProcessingService
    {
        private readonly IReservationService reservationService;

        public ReservationProcessingService(IReservationService reservationService)
        {
            this.reservationService = reservationService;
        }

        public async ValueTask<Reservation> CreateReservationAsync(CreateReservationModel model)
        {
            return await reservationService.CreateReservationAsync(model);
        }
    }
}

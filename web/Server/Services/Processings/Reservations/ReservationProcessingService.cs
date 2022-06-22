using FMFT.Web.Server.Services.Foundations.Reservations;
using FMFT.Web.Shared.Models.Reservations;
using FMFT.Web.Shared.Models.Reservations.Models;
using FMFT.Web.Shared.Models.Reservations.Params;

namespace FMFT.Web.Server.Services.Processings.Reservations
{
    public class ReservationProcessingService : IReservationProcessingService
    {
        private readonly IReservationService reservationService;

        public ReservationProcessingService(IReservationService reservationService)
        {
            this.reservationService = reservationService;
        }

        public async ValueTask<IEnumerable<Reservation>> RetrieveAllReservationsAsync()
        {
            return await reservationService.RetrieveAllReservationsAsync();
        }

        public async ValueTask<Reservation> RetrieveReservationByIdAsync(int reservationId)
        {
            return await reservationService.RetrieveReservationByIdAsync(reservationId);
        }

        public async ValueTask<Reservation> CreateReservationAsync(CreateReservationModel model)
        {
            CreateReservationParams @params = new()
            {
                ShowId = model.ShowId,
                SeatId = model.SeatId,
                UserId = model.UserId
            };

            return await reservationService.CreateReservationAsync(@params);
        }
    }
}

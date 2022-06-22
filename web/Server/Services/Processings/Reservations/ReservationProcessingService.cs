using FMFT.Web.Server.Brokers.Authentications;
using FMFT.Web.Server.Services.Foundations.Reservations;
using FMFT.Web.Shared.Models.Reservations;
using FMFT.Web.Shared.Models.Reservations.Models;
using FMFT.Web.Shared.Models.Reservations.Params;

namespace FMFT.Web.Server.Services.Processings.Reservations
{
    public class ReservationProcessingService : IReservationProcessingService
    {
        private readonly IReservationService reservationService;
        private readonly IAuthenticationBroker authenticationBroker;

        public ReservationProcessingService(IReservationService reservationService, 
            IAuthenticationBroker authenticationBroker)
        {
            this.reservationService = reservationService;
            this.authenticationBroker = authenticationBroker;
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

            if (@params.UserId == 0)
            {
                @params.UserId = authenticationBroker.AuthenticatedUserId;
            }

            return await reservationService.CreateReservationAsync(@params);
        }
    }
}

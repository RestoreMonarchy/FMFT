using FMFT.Web.Server.Brokers.Authentications;
using FMFT.Web.Server.Services.Foundations.Reservations;
using FMFT.Web.Server.Models.Reservations;
using FMFT.Web.Server.Models.Reservations.Requests;
using FMFT.Web.Server.Models.Reservations.Params;

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

        public async ValueTask<IEnumerable<Reservation>> RetrieveReservationsByUserIdAsync(int userId)
        {
            return await reservationService.RetrieveReservationsByUserIdAsync(userId);
        }

        public async ValueTask<Reservation> RetrieveReservationByIdAsync(int reservationId)
        {
            return await reservationService.RetrieveReservationByIdAsync(reservationId);
        }

        public async ValueTask<Reservation> CreateReservationAsync(CreateReservationParams @params)
        {
            return await reservationService.CreateReservationAsync(@params);
        }
    }
}

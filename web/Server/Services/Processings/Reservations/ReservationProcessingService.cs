using FMFT.Web.Server.Brokers.Authentications;
using FMFT.Web.Server.Services.Foundations.Reservations;
using FMFT.Web.Server.Models.Reservations;
using FMFT.Web.Server.Models.Reservations.Params;
using FMFT.Extensions.TheStandard;
using FMFT.Web.Server.Brokers.Loggings;

namespace FMFT.Web.Server.Services.Processings.Reservations
{
    public partial class ReservationProcessingService : TheStandardService, IReservationProcessingService
    {
        private readonly IReservationService reservationService;
        private readonly ILoggingBroker loggingBroker;

        public ReservationProcessingService(IReservationService reservationService,
            IAuthenticationBroker authenticationBroker,
            ILoggingBroker loggingBroker)
        {
            this.reservationService = reservationService;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<IEnumerable<Reservation>> RetrieveAllReservationsAsync()
            => TryCatch(async () =>
            {
                return await reservationService.RetrieveAllReservationsAsync();
            });

        public ValueTask<IEnumerable<Reservation>> RetrieveReservationsByUserIdAsync(int userId)
            => TryCatch(async () =>
            {
                return await reservationService.RetrieveReservationsByUserIdAsync(userId);
            });

        public ValueTask<IEnumerable<Reservation>> RetrieveReservationsByShowIdAsync(int showId)
            => TryCatch(async () =>
            {
                return await reservationService.RetrieveReservationsByShowIdAsync(showId);
            });

        public ValueTask<Reservation> RetrieveReservationByIdAsync(int reservationId)
            => TryCatch(async () =>
            {
                return await reservationService.RetrieveReservationByIdAsync(reservationId);
            });

        public ValueTask<Reservation> CreateReservationAsync(CreateReservationParams @params)
            => TryCatch(async () =>
            {
                return await reservationService.CreateReservationAsync(@params);
            });

        public ValueTask<Reservation> UpdateReservationStatusAsync(UpdateReservationStatusParams @params)
            => TryCatch(async () =>
            {
                return await reservationService.UpdateReservationStatusAsync(@params);
            });
    }
}

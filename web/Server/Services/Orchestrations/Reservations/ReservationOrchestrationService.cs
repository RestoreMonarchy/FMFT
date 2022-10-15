using FMFT.Extensions.TheStandard;
using FMFT.Web.Server.Brokers.Loggings;
using FMFT.Web.Server.Models.Reservations;
using FMFT.Web.Server.Models.Reservations.Params;
using FMFT.Web.Server.Models.Reservations.Requests;
using FMFT.Web.Server.Services.Processings.Reservations;

namespace FMFT.Web.Server.Services.Orchestrations.Reservations
{
    public partial class ReservationOrchestrationService : TheStandardService, IReservationOrchestrationService
    {
        private readonly IReservationProcessingService reservationService;
        private readonly ILoggingBroker loggingBroker;

        public ReservationOrchestrationService(IReservationProcessingService reservationService, ILoggingBroker loggingBroker)
        {
            this.reservationService = reservationService;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<Reservation> CreateReservationAsync(CreateReservationRequest request)
            => TryCatch(async () =>
            {
                CreateReservationParams @params = new()
                {
                    ShowId = request.ShowId,
                    SeatId = request.SeatId,
                    UserId = request.UserId
                };

                //await accountService.AuthorizeAccountByUserIdOrRolesAsync(@params.UserId, UserRole.Admin);

                return await reservationService.CreateReservationAsync(@params);
            });

        public ValueTask<Reservation> UpdateReservationStatusAsync(UpdateReservationStatusRequest request)
            => TryCatch(async () =>
            {
                //await accountService.AuthorizeAccountByRoleAsync(UserRole.Admin);

                UpdateReservationStatusParams @params = new()
                {
                    ReservationId = request.ReservationId,
                    ReservationStatus = request.Status,
                    UpdateStatusDate = DateTimeOffset.Now,
                    //AdminUserId = account.UserId
                };

                return await reservationService.UpdateReservationStatusAsync(@params);
            });

        public ValueTask<IEnumerable<Reservation>> RetrieveReservationsByUserIdAsync(int userId)
            => TryCatch(async () =>
            {
                //await accountService.AuthorizeAccountByUserIdOrRolesAsync(userId, UserRole.Admin);

                IEnumerable<Reservation> reservations = await reservationService.RetrieveReservationsByUserIdAsync(userId);

                return reservations;
            });

        public ValueTask<IEnumerable<Reservation>> RetrieveReservationsByShowIdAsync(int showId)
            => TryCatch(async () =>
            {
                //await accountService.AuthorizeAccountByRoleAsync(UserRole.Admin);

                IEnumerable<Reservation> reservations = await reservationService.RetrieveReservationsByShowIdAsync(showId);

                return reservations;
            });

        public ValueTask<IEnumerable<Reservation>> RetrieveAllReservationsAsync()
            => TryCatch(async () =>
            {
                //await accountService.AuthorizeAccountByRoleAsync(UserRole.Admin);

                IEnumerable<Reservation> reservations = await reservationService.RetrieveAllReservationsAsync();

                return reservations;
            });

        public ValueTask<Reservation> RetrieveReservationByIdAsync(int reservationId)
            => TryCatch(async () =>
            {
                Reservation reservation = await reservationService.RetrieveReservationByIdAsync(reservationId);

                //await accountService.AuthorizeAccountByUserIdOrRolesAsync(reservation.User.Id, UserRole.Admin);

                return reservation;
            });
    }
}

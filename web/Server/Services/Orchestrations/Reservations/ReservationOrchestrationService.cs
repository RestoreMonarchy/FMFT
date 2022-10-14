using FMFT.Web.Server.Models.Reservations;
using FMFT.Web.Server.Models.Reservations.Params;
using FMFT.Web.Server.Models.Reservations.Requests;
using FMFT.Web.Server.Services.Processings.Reservations;

namespace FMFT.Web.Server.Services.Orchestrations.Reservations
{
    public class ReservationOrchestrationService : IReservationOrchestrationService
    {
        private readonly IReservationProcessingService reservationService;

        public ReservationOrchestrationService(IReservationProcessingService reservationService)
        {
            this.reservationService = reservationService;
        }

        public async ValueTask<Reservation> CreateReservationAsync(CreateReservationRequest request)
        {
            CreateReservationParams @params = new()
            {
                ShowId = request.ShowId,
                SeatId = request.SeatId,
                UserId = request.UserId
            };

            //await accountService.AuthorizeAccountByUserIdOrRolesAsync(@params.UserId, UserRole.Admin);

            return await reservationService.CreateReservationAsync(@params);
        }

        public async ValueTask<Reservation> UpdateReservationStatusAsync(UpdateReservationStatusRequest request)
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
        }

        public async ValueTask<IEnumerable<Reservation>> RetrieveReservationsByUserIdAsync(int userId)
        {
            //await accountService.AuthorizeAccountByUserIdOrRolesAsync(userId, UserRole.Admin);

            IEnumerable<Reservation> reservations = await reservationService.RetrieveReservationsByUserIdAsync(userId);

            return reservations;
        }

        public async ValueTask<IEnumerable<Reservation>> RetrieveReservationsByShowIdAsync(int showId)
        {
            //await accountService.AuthorizeAccountByRoleAsync(UserRole.Admin);

            IEnumerable<Reservation> reservations = await reservationService.RetrieveReservationsByShowIdAsync(showId);

            return reservations;
        }

        public async ValueTask<IEnumerable<Reservation>> RetrieveAllReservationsAsync()
        {
            //await accountService.AuthorizeAccountByRoleAsync(UserRole.Admin);

            IEnumerable<Reservation> reservations = await reservationService.RetrieveAllReservationsAsync();

            return reservations;
        }

        public async ValueTask<Reservation> RetrieveReservationByIdAsync(int reservationId)
        {
            Reservation reservation = await reservationService.RetrieveReservationByIdAsync(reservationId);

            //await accountService.AuthorizeAccountByUserIdOrRolesAsync(reservation.User.Id, UserRole.Admin);

            return reservation;
        }
    }
}

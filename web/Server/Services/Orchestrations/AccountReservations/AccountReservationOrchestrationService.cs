using FMFT.Web.Server.Models.Reservations;
using FMFT.Web.Server.Models.Reservations.Params;
using FMFT.Web.Server.Models.Reservations.Requests;
using FMFT.Web.Server.Services.Processings.Accounts;
using FMFT.Web.Server.Services.Processings.Reservations;
using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Server.Services.Orchestrations.AccountReservations
{
    public class AccountReservationOrchestrationService : IAccountReservationOrchestrationService
    {
        private readonly IAccountProcessingService accountService;
        private readonly IReservationProcessingService reservationService;

        public AccountReservationOrchestrationService(IAccountProcessingService accountService, IReservationProcessingService reservationService)
        {
            this.accountService = accountService;
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

            accountService.AuthorizeAccountByUserIdOrRoles(@params.UserId, UserRole.Admin);

            return await reservationService.CreateReservationAsync(@params);
        }

        public async ValueTask<IEnumerable<Reservation>> RetrieveReservationsByUserIdAsync(int userId)
        {
            accountService.AuthorizeAccountByUserIdOrRoles(userId, UserRole.Admin);

            IEnumerable<Reservation> reservations = await reservationService.RetrieveReservationsByUserIdAsync(userId);

            return reservations;
        }

        public async ValueTask<IEnumerable<Reservation>> RetrieveReservationsByShowIdAsync(int showId)
        {
            accountService.AuthorizeAccountByRole(UserRole.Admin);

            IEnumerable<Reservation> reservations = await reservationService.RetrieveReservationsByShowIdAsync(showId);

            return reservations;
        }

        public async ValueTask<IEnumerable<Reservation>> RetrieveAllReservationsAsync()
        {
            accountService.AuthorizeAccountByRole(UserRole.Admin);

            IEnumerable<Reservation> reservations = await reservationService.RetrieveAllReservationsAsync();

            return reservations;
        }

        public async ValueTask<Reservation> RetrieveReservationByIdAsync(int reservationId)
        {
            Reservation reservation = await reservationService.RetrieveReservationByIdAsync(reservationId);

            accountService.AuthorizeAccountByUserIdOrRoles(reservation.User.Id, UserRole.Admin);

            return reservation;
        }
    }
}

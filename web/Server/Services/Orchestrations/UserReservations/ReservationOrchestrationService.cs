using FMFT.Web.Server.Services.Processings.Accounts;
using FMFT.Web.Server.Services.Processings.Reservations;
using FMFT.Web.Server.Services.Processings.Users;
using FMFT.Web.Shared.Models.Reservations;
using FMFT.Web.Shared.Models.Reservations.Requests;
using FMFT.Web.Shared.Models.Reservations.Params;
using FMFT.Web.Shared.Models.Shared.Enums;

namespace FMFT.Web.Server.Services.Orchestrations.Reservations
{
    public class ReservationOrchestrationService : IReservationOrchestrationService
    {
        private readonly IAccountProcessingService accountService;
        private readonly IReservationProcessingService reservationService;

        public ReservationOrchestrationService(IAccountProcessingService accountService, IReservationProcessingService reservationService)
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
    }
}

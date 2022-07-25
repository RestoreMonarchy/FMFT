using FMFT.Web.Server.Services.Processings.Reservations;
using FMFT.Web.Server.Services.Processings.Users;
using FMFT.Web.Shared.Models.Reservations;
using FMFT.Web.Shared.Models.Reservations.Models;
using FMFT.Web.Shared.Models.Reservations.Params;
using FMFT.Web.Shared.Models.Users;

namespace FMFT.Web.Server.Services.Orchestrations.UserReservations
{
    public class UserReservationOrchestrationService : IUserReservationOrchestrationService
    {
        private readonly IUserProcessingService userService;
        private readonly IReservationProcessingService reservationService;

        public UserReservationOrchestrationService(
            IUserProcessingService userService, 
            IReservationProcessingService reservationService)
        {
            this.userService = userService;
            this.reservationService = reservationService;
        }

        public async ValueTask<Reservation> CreateReservationAsync(CreateReservationModel model)
        {
            CreateReservationParams @params = new()
            {
                ShowId = model.ShowId,
                SeatId = model.SeatId,
                UserId = model.UserId
            };

            await userService.AuthorizeAuthenticatedUserByIdOrRolesAsync(@params.UserId, UserRole.Admin);

            return await reservationService.CreateReservationAsync(@params);
        }

        public async ValueTask<IEnumerable<Reservation>> RetrieveReservationsByUserIdAsync(int userId)
        {
            await userService.AuthorizeAuthenticatedUserByIdOrRolesAsync(userId, UserRole.Admin);

            IEnumerable<Reservation> reservations = await reservationService.RetrieveReservationsByUserIdAsync(userId);
            return reservations;
        }
    }
}

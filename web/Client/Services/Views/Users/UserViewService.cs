using FMFT.Web.Client.Models.Reservations;
using FMFT.Web.Client.Models.Users;
using FMFT.Web.Client.Services.Foundations.Reservations;
using FMFT.Web.Client.Services.Foundations.Users;

namespace FMFT.Web.Client.Services.Views.Users
{
    public class UserViewService : IUserViewService
    {
        private readonly IUserService userService;
        private readonly IReservationService reservationService;

        public UserViewService(IUserService userService, IReservationService reservationService)
        {
            this.userService = userService;
            this.reservationService = reservationService;
        }

        public async ValueTask<List<User>> RetrieveAllUsersAsync()
        {
            return await userService.RetrieveAllUsersAsync();
        }

        public async ValueTask<User> RetrieveUserByIdAsync(int userId)
        {
            return await userService.RetrieveUserByIdAsync(userId);
        }

        public async ValueTask<List<Reservation>> RetrieveReservationsByUserIdAsync(int userId)
        {
            return await reservationService.RetrieveReservationsByUserIdAsync(userId);
        }
    }
}

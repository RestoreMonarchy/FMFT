using FMFT.Web.Client.Models.Reservations;
using FMFT.Web.Client.Models.Users;
using FMFT.Web.Client.Models.Users.Requests;

namespace FMFT.Web.Client.Services.Views.Users
{
    public interface IUserViewService
    {
        ValueTask<List<User>> RetrieveAllUsersAsync();
        ValueTask<List<Reservation>> RetrieveReservationsByUserIdAsync(int userId);
        ValueTask<User> RetrieveUserByIdAsync(int userId);
        ValueTask UpdateUserRoleAsync(UpdateUserRoleRequest request);
    }
}
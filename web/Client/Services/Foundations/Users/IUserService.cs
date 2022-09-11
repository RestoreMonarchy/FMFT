using FMFT.Web.Client.Models.Users;
using FMFT.Web.Client.Models.Users.Requests;

namespace FMFT.Web.Client.Services.Foundations.Users
{
    public interface IUserService
    {
        ValueTask<List<User>> RetrieveAllUsersAsync();
        ValueTask<User> RetrieveUserByIdAsync(int userId);
        ValueTask UpdateUserCultureAsync(UpdateUserCultureRequest request);
        ValueTask UpdateUserRoleAsync(UpdateUserRoleRequest request);
    }
}
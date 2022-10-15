using FMFT.Web.Client.Models.API.Users;
using FMFT.Web.Client.Models.API.Users.Requests;

namespace FMFT.Web.Client.Brokers.APIs
{
    public partial interface IAPIBroker
    {
        ValueTask<List<User>> GetAllUsersAsync();
        ValueTask<User> GetUserByIdAsync(int userId);
        ValueTask UpdateUserCultureAsync(UpdateUserCultureRequest request);
        ValueTask UpdateUserRoleAsync(UpdateUserRoleRequest request);
    }
}

using FMFT.Web.Client.Models.Users;
using FMFT.Web.Client.Models.Users.Requests;

namespace FMFT.Web.Client.Brokers.APIs
{
    public partial interface IAPIBroker
    {
        ValueTask<List<User>> GetAllUsersAsync();
        ValueTask<User> GetUserByIdAsync(int userId);
        ValueTask UpdateUserRoleAsync(UpdateUserRoleRequest request);
    }
}

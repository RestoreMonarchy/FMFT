using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Users;
using FMFT.Web.Client.Models.API.Users.Requests;

namespace FMFT.Web.Client.Brokers.APIs
{
    public partial interface IAPIBroker
    {
        ValueTask<APIResponse<List<User>>> GetAllUsersAsync();
        ValueTask<APIResponse<User>> GetUserByIdAsync(int userId);
        ValueTask<APIResponse> UpdateUserCultureAsync(UpdateUserCultureRequest request);
        ValueTask<APIResponse> UpdateUserRoleAsync(UpdateUserRoleRequest request);
    }
}

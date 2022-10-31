using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Users;
using FMFT.Web.Client.Models.API.Users.Requests;

namespace FMFT.Web.Client.Brokers.APIs
{
    public partial class APIBroker
    {
        private const string UsersRelativeUrl = "api/users";

        public async ValueTask<APIResponse<List<User>>> GetAllUsersAsync()
        {
            return await GetAsync<List<User>>(UsersRelativeUrl);
        }

        public async ValueTask<APIResponse<User>> GetUserByIdAsync(int userId)
        {
            string url = $"{UsersRelativeUrl}/{userId}";
            return await GetAsync<User>(url);
        }

        public async ValueTask<APIResponse> UpdateUserRoleAsync(UpdateUserRoleRequest request)
        {
            string url = $"{UsersRelativeUrl}/{request.UserId}/updaterole";
            return await PostAsync(url, request);
        }

        public async ValueTask<APIResponse> UpdateUserCultureAsync(UpdateUserCultureRequest request)
        {
            string url = $"{UsersRelativeUrl}/{request.UserId}/updateculture";
            return await PostAsync(url, request);
        }

        public async ValueTask<APIResponse> ConfirmUserEmailAsync(ConfirmUserEmailRequest request)
        {
            string url = $"{UsersRelativeUrl}/{request.UserId}/confirmemail/{request.ConfirmSecret}";
            return await PostAsync(url, null);
        }
    }
}

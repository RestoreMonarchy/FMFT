using FMFT.Web.Client.Models.API.Users;
using FMFT.Web.Client.Models.API.Users.Requests;

namespace FMFT.Web.Client.Brokers.APIs
{
    public partial class APIBroker
    {
        private const string UsersRelativeUrl = "api/users";

        public async ValueTask<List<User>> GetAllUsersAsync()
        {
            return await GetAsync<List<User>>(UsersRelativeUrl);
        }

        public async ValueTask<User> GetUserByIdAsync(int userId)
        {
            string url = $"{UsersRelativeUrl}/{userId}";
            return await GetAsync<User>(url);
        }

        public async ValueTask UpdateUserRoleAsync(UpdateUserRoleRequest request)
        {
            string url = $"{UsersRelativeUrl}/{request.UserId}/updaterole";
            await PostContentWithNoResponseAsync(url, request);
        }

        public async ValueTask UpdateUserCultureAsync(UpdateUserCultureRequest request)
        {
            string url = $"{UsersRelativeUrl}/{request.UserId}/updateculture";
            await PostContentWithNoResponseAsync(url, request);
        }
    }
}

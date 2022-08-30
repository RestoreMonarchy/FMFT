using FMFT.Web.Client.Models.Users;

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
    }
}

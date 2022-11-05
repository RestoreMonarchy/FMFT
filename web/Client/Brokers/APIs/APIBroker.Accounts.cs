using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Accounts;
using FMFT.Web.Client.Models.API.Accounts.Requests;

namespace FMFT.Web.Client.Brokers.APIs
{
    public partial class APIBroker
    {
        private const string AccountsRelativeUrl = "account";

        public async ValueTask<APIResponse<UserAccount>> GetUserAccountAsync()
        {
            string url = $"{AccountsRelativeUrl}/user";

            return await GetAsync<UserAccount>(url);
        }

        public async ValueTask<APIResponse<AccountToken>> PostAccountLoginAsync(LogInWithPasswordRequest request)
        {
            string url = $"{AccountsRelativeUrl}/login";

            return await PostAsync<AccountToken>(url, request);
        }

        public async ValueTask<APIResponse<AccountToken>> PostAccountRegisterAsync(RegisterWithPasswordRequest request)
        {
            string url = $"{AccountsRelativeUrl}/register";

            return await PostAsync<AccountToken>(url, request);
        }

        public async ValueTask<APIResponse> PostAccountChangePasswordAsync(ChangePasswordRequest request)
        {
            string url = $"{AccountsRelativeUrl}/changepassword";

            return await PostAsync(url, request);
        }

        public async ValueTask<APIResponse<AccountToken>> PostAccountFacebookLoginAsync(FacebookLoginRequest request)
        {
            string url = $"{AccountsRelativeUrl}/login/facebook";

            return await PostAsync<AccountToken>(url, request);
        }
    }
}

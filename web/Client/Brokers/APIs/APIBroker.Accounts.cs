using FMFT.Web.Client.Models.API.Accounts;
using FMFT.Web.Client.Models.API.Accounts.Requests;

namespace FMFT.Web.Client.Brokers.APIs
{
    public partial class APIBroker
    {
        private const string AccountsRelativeUrl = "account";

        public async ValueTask<UserAccount> GetUserAccountAsync()
        {
            string url = $"{AccountsRelativeUrl}/user";
            return await GetAsync<UserAccount>(url);
        }

        public async ValueTask<AccountToken> PostAccountLoginAsync(LogInWithPasswordRequest request)
        {
            string url = $"{AccountsRelativeUrl}/login";
            return await PostAsync<LogInWithPasswordRequest, AccountToken>(url, request);
        }

        public async ValueTask<AccountToken> PostAccountRegisterAsync(RegisterWithPasswordRequest request)
        {
            string url = $"{AccountsRelativeUrl}/register";
            return await PostAsync<RegisterWithPasswordRequest, AccountToken>(url, request);
        }
    }
}

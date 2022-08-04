using FMFT.Web.Client.Models.Accounts;
using FMFT.Web.Client.Models.Accounts.Requests;

namespace FMFT.Web.Client.Brokers.APIs
{
    public partial class APIBroker
    {
        private const string AccountsRelativeUrl = "account";

        public async ValueTask<Account> GetAccountInfoAsync()
        {
            string url = $"{AccountsRelativeUrl}/info";
            return await GetAsync<Account>(url);
        }

        public async ValueTask<Account> PostAccountLoginAsync(LogInWithPasswordRequest request)
        {
            string url = $"{AccountsRelativeUrl}/login";
            return await PostAsync<LogInWithPasswordRequest, Account>(url, request);
        }

        public async ValueTask<Account> PostAccountRegisterAsync(RegisterWithPasswordRequest request)
        {
            string url = $"{AccountsRelativeUrl}/register";
            return await PostAsync<RegisterWithPasswordRequest, Account>(url, request);
        }

        public async ValueTask<Account> PostConfirmExternalLoginAsync(ConfirmExternalLoginRequest request)
        {
            string url = $"{AccountsRelativeUrl}/externalloginconfirmation";
            return await PostAsync<ConfirmExternalLoginRequest, Account>(url, request);
        }
    }
}

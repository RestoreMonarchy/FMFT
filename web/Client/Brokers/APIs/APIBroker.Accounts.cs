using FMFT.Web.Shared.Models.Accounts;
using FMFT.Web.Shared.Models.Accounts.Requests;

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

        public async ValueTask<Account> PostAccountLoginAsync(SignInUserWithPasswordRequest request)
        {
            string url = $"{AccountsRelativeUrl}/login";
            return await PostAsync<SignInUserWithPasswordRequest, Account>(url, request);
        }

        public async ValueTask<Account> PostAccountRegisterAsync(RegisterUserWithPasswordRequest request)
        {
            string url = $"{AccountsRelativeUrl}/register";
            return await PostAsync<RegisterUserWithPasswordRequest, Account>(url, request);
        }

        public async ValueTask<Account> PostExternalLoginConfirmationAsync(ExternalLoginConfirmationRequest request)
        {
            string url = $"{AccountsRelativeUrl}/externalloginconfirmation";
            return await PostAsync<ExternalLoginConfirmationRequest, Account>(url, request);
        }
    }
}

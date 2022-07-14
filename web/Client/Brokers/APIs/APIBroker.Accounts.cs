using FMFT.Web.Shared.Models.Users;
using FMFT.Web.Shared.Models.Users.Models;

namespace FMFT.Web.Client.Brokers.APIs
{
    public partial class APIBroker
    {
        private const string AccountsRelativeUrl = "account";

        public async ValueTask<UserInfo> GetAccountInfoAsync()
        {
            string url = $"{AccountsRelativeUrl}/info";
            return await GetAsync<UserInfo>(url);
        }

        public async ValueTask<UserInfo> PostAccountLoginAsync(SignInUserWithPasswordModel model)
        {
            string url = $"{AccountsRelativeUrl}/login";
            return await PostAsync<SignInUserWithPasswordModel, UserInfo>(url, model);
        }

        public async ValueTask<UserInfo> PostAccountRegisterAsync(RegisterUserWithPasswordModel model)
        {
            string url = $"{AccountsRelativeUrl}/register";
            return await PostAsync<RegisterUserWithPasswordModel, UserInfo>(url, model);
        }

        public async ValueTask<UserInfo> PostExternalLoginConfirmationAsync(ExternalLoginConfirmationModel model)
        {
            string url = $"{AccountsRelativeUrl}/externalloginconfirmation";
            return await PostAsync<ExternalLoginConfirmationModel, UserInfo>(url, model);
        }
    }
}

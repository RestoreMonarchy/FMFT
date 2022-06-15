using FMFT.Web.Shared.Models.Users;
using FMFT.Web.Shared.Models.Users.Models;

namespace FMFT.Web.Client.Brokers.APIs
{
    public partial class APIBroker
    {
        private const string AccountsRelativeUrl = "account";

        public async ValueTask<UserInfo> GetAccountInfoAsync()
        {
            return await GetAsync<UserInfo>($"{AccountsRelativeUrl}/info");
        }

        public async ValueTask<UserInfo> PostAccountLoginAsync(SignInUserWithPasswordModel model)
        {
            return await PostAsync<SignInUserWithPasswordModel, UserInfo>($"{AccountsRelativeUrl}/login", model);
        }

        public async ValueTask<UserInfo> PostAccountRegisterAsync(RegisterUserWithPasswordModel model)
        {
            return await PostAsync<RegisterUserWithPasswordModel, UserInfo>($"{AccountsRelativeUrl}/register", model);
        }
    }
}

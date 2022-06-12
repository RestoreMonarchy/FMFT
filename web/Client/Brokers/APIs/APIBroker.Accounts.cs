using FMFT.Web.Shared.Models.Users.Models;

namespace FMFT.Web.Client.Brokers.APIs
{
    public partial class APIBroker
    {
        private const string AccountsRelativeUrl = "account";

        public async ValueTask PostAccountLoginAsync(SignInUserWithPasswordModel model)
        {
            await PostContentWithNoResponseAsync($"{AccountsRelativeUrl}/login", model);
        }

        public async ValueTask PostAccountRegisterAsync(RegisterUserWithPasswordModel model)
        {
            await PostContentWithNoResponseAsync($"{AccountsRelativeUrl}/register", model);
        }
    }
}

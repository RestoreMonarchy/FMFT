using FMFT.Web.Shared.Models.Users;
using FMFT.Web.Shared.Models.Users.Models;

namespace FMFT.Web.Client.Brokers.APIs
{
    public partial interface IAPIBroker
    {
        ValueTask<UserInfo> GetAccountInfoAsync();
        ValueTask<UserInfo> PostAccountLoginAsync(SignInUserWithPasswordModel model);
        ValueTask<UserInfo> PostAccountRegisterAsync(RegisterUserWithPasswordModel model);
    }
}

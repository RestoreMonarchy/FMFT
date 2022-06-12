using FMFT.Web.Shared.Models.Users.Models;

namespace FMFT.Web.Client.Brokers.APIs
{
    public partial interface IAPIBroker
    {
        ValueTask PostAccountLoginAsync(SignInUserWithPasswordModel model);
        ValueTask PostAccountRegisterAsync(RegisterUserWithPasswordModel model);
    }
}

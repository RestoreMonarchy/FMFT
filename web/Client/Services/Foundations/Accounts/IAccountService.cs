using FMFT.Web.Shared.Models.Users;
using FMFT.Web.Shared.Models.Users.Models;

namespace FMFT.Web.Client.Services.Foundations.Accounts
{
    public interface IAccountService
    {
        ValueTask<UserInfo> LoginAsync(SignInUserWithPasswordModel model);
        ValueTask<UserInfo> RegisterAsync(RegisterUserWithPasswordModel model);
        ValueTask<UserInfo> RetrieveAccountInfoAsync();
    }
}

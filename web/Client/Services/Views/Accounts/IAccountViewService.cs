using FMFT.Web.Shared.Models.Users;
using FMFT.Web.Shared.Models.Users.Models;

namespace FMFT.Web.Client.Services.Views.Accounts
{
    public interface IAccountViewService
    {
        UserInfo UserInfo { get; }

        void ForceLoadNavigateTo(string url);
        ValueTask LoginAsync(SignInUserWithPasswordModel model);
        ValueTask RegisterAsync(RegisterUserWithPasswordModel model);
    }
}
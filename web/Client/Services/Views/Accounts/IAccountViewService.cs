using FMFT.Web.Shared.Models.Users.Models;

namespace FMFT.Web.Client.Services.Views.Accounts
{
    public interface IAccountViewService
    {
        ValueTask LoginAsync(SignInUserWithPasswordModel model);
        ValueTask RegisterAsync(RegisterUserWithPasswordModel model);
    }
}
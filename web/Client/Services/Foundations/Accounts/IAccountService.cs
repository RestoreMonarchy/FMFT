using FMFT.Web.Shared.Models.Users.Models;

namespace FMFT.Web.Client.Services.Foundations.Accounts
{
    public interface IAccountService
    {
        ValueTask LoginAsync(SignInUserWithPasswordModel model);
        ValueTask RegisterAsync(RegisterUserWithPasswordModel model);
    }
}

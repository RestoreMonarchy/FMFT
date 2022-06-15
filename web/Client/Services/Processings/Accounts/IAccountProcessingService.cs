using FMFT.Web.Shared.Models.Users;
using FMFT.Web.Shared.Models.Users.Models;

namespace FMFT.Web.Client.Services.Processings.Accounts
{
    public interface IAccountProcessingService
    {
        UserInfo UserInfo { get; }
        bool IsAuthenticated { get; }

        event Action UserInfoChanged;

        ValueTask InitializeAsync();
        ValueTask LoginAsync(SignInUserWithPasswordModel model);
        ValueTask RegisterAsync(RegisterUserWithPasswordModel model);
        ValueTask ReloadUserInfoAsync();
    }
}
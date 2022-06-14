using FMFT.Web.Shared.Models.Users;
using FMFT.Web.Shared.Models.Users.Models;

namespace FMFT.Web.Server.Services.Processings.Users
{
    public interface IUserProcessingService
    {
        ValueTask<User> GetAuthenticatedUserAsync();
        ValueTask<UserInfo> GetAuthenticatedUserInfoAsync();
        ValueTask RegisterUserWithPasswordAsync(RegisterUserWithPasswordModel model);
        ValueTask SignInUserWithPasswordAsync(SignInUserWithPasswordModel model);
        ValueTask SignOutUserAsync();
    }
}
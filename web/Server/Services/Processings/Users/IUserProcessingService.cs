using FMFT.Web.Shared.Models.Users;
using FMFT.Web.Shared.Models.Users.Models;

namespace FMFT.Web.Server.Services.Processings.Users
{
    public interface IUserProcessingService
    {
        ValueTask ChallengeExternalLoginAsync(string provider, string redirectUrl);
        ValueTask<User> GetAuthenticatedUserAsync();
        ValueTask<UserInfo> GetAuthenticatedUserInfoAsync();
        ValueTask HandleExternalLoginCallbackAsync();
        ValueTask<UserInfo> ConfirmExternalLoginAsync(ExternalLoginConfirmationModel model);
        ValueTask<UserInfo> RegisterUserWithPasswordAsync(RegisterUserWithPasswordModel model);
        ValueTask<UserInfo> SignInUserWithPasswordAsync(SignInUserWithPasswordModel model);
        ValueTask SignOutUserAsync();
    }
}
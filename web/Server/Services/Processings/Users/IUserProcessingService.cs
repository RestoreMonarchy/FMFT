using FMFT.Web.Shared.Models.Users.Models;

namespace FMFT.Web.Server.Services.Processings.Users
{
    public interface IUserProcessingService
    {
        ValueTask RegisterUserWithPasswordAsync(RegisterUserWithPasswordModel model);
        ValueTask SignInUserWithPasswordAsync(SignInUserWithPasswordModel model);
        ValueTask SignOutUserAsync();
    }
}
using FMFT.Web.Shared.Models.Users;
using FMFT.Web.Shared.Models.Users.Params;

namespace FMFT.Web.Server.Services.Foundations.Users
{
    public interface IUserService
    {
        bool IsUserAuthenticated();
        ValueTask<User> RegisterUserWithLoginAsync(RegisterUserWithLoginParams @params);
        ValueTask<User> RegisterUserWithPasswordAsync(RegisterUserWithPasswordParams @params);
        ValueTask<IEnumerable<User>> RetrieveAllUsersAsync();
        int RetrieveAuthenticatedUserId();
        ValueTask<User> RetrieveUserByEmailAsync(string email);
        ValueTask<User> RetrieveUserByIdAsync(int userId);
        ValueTask<User> RetrieveUserByLoginAsync(string providerName, string providerKey);
    }
}

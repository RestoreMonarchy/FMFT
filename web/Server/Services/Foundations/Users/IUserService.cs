using FMFT.Web.Shared.Models.Users;
using FMFT.Web.Shared.Models.Users.Params;

namespace FMFT.Web.Server.Services.Foundations.Users
{
    public interface IUserService
    {
        ValueTask<User> RegisterUserWithLoginAsync(RegisterUserWithLoginParams @params);
        ValueTask<User> RegisterUserWithPasswordAsync(RegisterUserWithPasswordParams @params);
        ValueTask<IEnumerable<User>> RetrieveAllUsersAsync();
        ValueTask<User> RetrieveUserByEmailAsync(string email);
        ValueTask<User> RetrieveUserByIdAsync(int userId);
    }
}

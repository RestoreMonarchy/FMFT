using FMFT.Web.Server.Models.Users;
using FMFT.Web.Server.Models.Users.Params;

namespace FMFT.Web.Server.Services.Foundations.Users
{
    public interface IUserService
    {
        ValueTask<User> RegisterUserWithLoginAsync(RegisterUserWithLoginParams @params);
        ValueTask<User> RegisterUserWithPasswordAsync(RegisterUserWithPasswordProcessingParams args);
        ValueTask<IEnumerable<User>> RetrieveAllUsersAsync();
        ValueTask<User> RetrieveUserByEmailAsync(string email);
        ValueTask<User> RetrieveUserByIdAsync(int userId);
        ValueTask<User> RetrieveUserByLoginAsync(string providerName, string providerKey);
        ValueTask UpdateUserCultureAsync(UpdateUserCultureParams @params);
        ValueTask UpdateUserRoleAsync(UpdateUserRoleParams @params);
    }
}

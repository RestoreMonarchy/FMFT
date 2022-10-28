using FMFT.Web.Server.Models.Users;
using FMFT.Web.Server.Models.Users.Params;
using FMFT.Web.Server.Models.Users.Requests;

namespace FMFT.Web.Server.Services.Foundations.Users
{
    public interface IUserService
    {
        ValueTask<User> RegisterUserWithLoginAsync(RegisterUserWithLoginParams @params);
        ValueTask<User> RegisterUserWithPasswordAsync(RegisterUserWithPasswordRequest request);
        ValueTask<IEnumerable<User>> RetrieveAllUsersAsync();
        ValueTask<User> RetrieveUserByEmailAndPasswordAsync(string email, string passwordText);
        ValueTask<User> RetrieveUserByEmailAsync(string email);
        ValueTask<User> RetrieveUserByIdAsync(int userId);
        ValueTask<User> RetrieveUserByLoginAsync(string providerName, string providerKey);
        ValueTask UpdateUserCultureAsync(UpdateUserCultureParams @params);
        ValueTask UpdateUserRoleAsync(UpdateUserRoleParams @params);
    }
}

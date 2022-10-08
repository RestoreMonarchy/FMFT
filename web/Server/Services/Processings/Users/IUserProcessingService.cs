using FMFT.Web.Server.Models.Users;
using FMFT.Web.Server.Models.Users.Params;

namespace FMFT.Web.Server.Services.Processings.Users
{
    public interface IUserProcessingService
    {
        ValueTask<User> RetrieveUserByIdAsync(int userId);
        ValueTask<User> RetrieveUserByLoginAsync(string providerName, string providerKey);
        ValueTask<User> RetrieveUserByEmailAsync(string email);
        ValueTask<IEnumerable<User>> RetrieveAllUsersAsync();
        ValueTask<User> RetrieveUserByEmailAndPasswordAsync(string email, string passwordText);
        ValueTask<User> RegisterUserWithPasswordAsync(RegisterUserWithPasswordProcessingParams args);
        ValueTask<User> RegisterUserWithLoginAsync(RegisterUserWithLoginParams @params);
        ValueTask UpdateUserRoleAsync(UpdateUserRoleParams @params);
        ValueTask UpdateUserCultureAsync(UpdateUserCultureParams @params);
    }
}
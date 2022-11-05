using FMFT.Web.Server.Models.Accounts;
using FMFT.Web.Server.Models.UserAccounts;
using FMFT.Web.Server.Models.UserAccounts.Requests;
using FMFT.Web.Server.Models.Users;
using FMFT.Web.Server.Models.Users.Params;
using FMFT.Web.Server.Models.Users.Requests;
using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Server.Services.Orchestrations.UserAccounts
{
    public interface IUserAccountOrchestrationService
    {
        ValueTask AuthorizeAccountAsync();
        ValueTask AuthorizeAccountByUserIdAsync(int userId);
        ValueTask AuthorizeUserAccountByRoleAsync(params UserRole[] authorizedRoles);
        ValueTask AuthorizeUserAccountByUserIdOrRolesAsync(int userId, params UserRole[] authorizedRoles);
        ValueTask ChangeUserAccountPasswordAsync(ChangeUserAccountPasswordRequest request);
        ValueTask ConfirmEmailAsync(int userId, Guid secretKey);
        ValueTask<AccountToken> LoginWithFacebookAsync(LoginWithFacebookRequest request);
        ValueTask<AccountToken> RegisterWithPasswordAsync(RegisterUserWithPasswordRequest request);
        ValueTask<Account> RetrieveAccountAsync();
        ValueTask<IEnumerable<User>> RetrieveAllUsersAsync();
        ValueTask<UserAccount> RetrieveUserAccountAsync();
        ValueTask<User> RetrieveUserByIdAsync(int userId);
        ValueTask<AccountToken> SignInWithPasswordAsync(SignInWithPasswordRequest request);
        ValueTask UpdateUserCultureAsync(UpdateUserCultureParams @params);
        ValueTask UpdateUserPasswordAsync(UpdateUserPasswordRequest request);
        ValueTask UpdateUserRoleAsync(UpdateUserRoleParams @params);
    }
}

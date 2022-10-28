using FMFT.Web.Server.Brokers.Loggings;
using FMFT.Web.Server.Models.Accounts;
using FMFT.Web.Server.Models.Accounts.Params;
using FMFT.Web.Server.Models.UserAccounts;
using FMFT.Web.Server.Models.UserAccounts.Exceptions;
using FMFT.Web.Server.Models.UserAccounts.Requests;
using FMFT.Web.Server.Models.Users;
using FMFT.Web.Server.Models.Users.Params;
using FMFT.Web.Server.Models.Users.Requests;
using FMFT.Web.Server.Services.Foundations.Accounts;
using FMFT.Web.Server.Services.Foundations.Users;
using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Server.Services.Orchestrations.UserAccounts
{
    public partial class UserAccountOrchestrationService : IUserAccountOrchestrationService
    {
        private readonly IAccountService accountService;
        private readonly IUserService userService;
        private readonly ILoggingBroker loggingBroker;

        public UserAccountOrchestrationService(
            IAccountService accountService, 
            IUserService userService, 
            ILoggingBroker loggingBroker)
        {
            this.accountService = accountService;
            this.userService = userService;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<IEnumerable<User>> RetrieveAllUsersAsync()
        {
            await AuthorizeUserAccountByRoleAsync(UserRole.Admin);

            return await userService.RetrieveAllUsersAsync();
        }

        public async ValueTask<User> RetrieveUserByIdAsync(int userId)
        {
            await AuthorizeUserAccountByUserIdOrRolesAsync(userId, UserRole.Admin);

            return await userService.RetrieveUserByIdAsync(userId);
        }

        public async ValueTask<AccountToken> RegisterWithPasswordAsync(RegisterUserWithPasswordRequest request)
        {
            User user = await userService.RegisterUserWithPasswordAsync(request);
            Account account = MapUserToAccount(user);

            CreateTokenParams @params = new()
            {
                Account = account,
                AuthenticationMethod = null
            };

            return await accountService.CreateTokenAsync(@params);
        }

        public async ValueTask<AccountToken> SignInWithPasswordAsync(SignInWithPasswordRequest request)
        {
            User user = await userService.RetrieveUserByEmailAndPasswordAsync(request.Email, request.PasswordText);
            Account account = MapUserToAccount(user);

            CreateTokenParams @params = new()
            {
                Account = account,
                AuthenticationMethod = null
            };

            return await accountService.CreateTokenAsync(@params);
        }

        public async ValueTask UpdateUserRoleAsync(UpdateUserRoleParams @params)
        {
            await AuthorizeUserAccountByRoleAsync(UserRole.Admin);
            await userService.UpdateUserRoleAsync(@params);
        }

        public async ValueTask UpdateUserCultureAsync(UpdateUserCultureParams @params)
        {
            await accountService.AuthorizeAccountByUserIdAsync(@params.UserId);
            await userService.UpdateUserCultureAsync(@params);
        }

        public async ValueTask<UserAccount> RetrieveUserAccountAsync()
        {
            Account account = await accountService.RetrieveAccountAsync();
            User user = await userService.RetrieveUserByIdAsync(account.UserId);
            UserAccount userAccount = MapUserToUserAccount(user);

            return userAccount;
        }

        public async ValueTask AuthorizeAccountAsync()
        {
            await accountService.AuthorizeAccountAsync();
        }

        public async ValueTask AuthorizeAccountByUserIdAsync(int userId)
        {
            await accountService.AuthorizeAccountByUserIdAsync(userId);
        }

        public async ValueTask AuthorizeUserAccountByRoleAsync(params UserRole[] authorizedRoles)
        {
            UserAccount userAccount = await RetrieveUserAccountAsync();

            if (!authorizedRoles.Contains(userAccount.Role))
            {
                throw new NotAuthorizedUserAccountException();
            }
        }

        public async ValueTask AuthorizeUserAccountByUserIdOrRolesAsync(int userId, params UserRole[] authorizedRoles)
        {
            UserAccount userAccount = await RetrieveUserAccountAsync();

            if (userAccount.UserId != userId && !authorizedRoles.Contains(userAccount.Role))
            {
                throw new NotAuthorizedUserAccountException();
            }
        }

        public async ValueTask<Account> RetrieveAccountAsync()
        {
            return await accountService.RetrieveAccountAsync();
        }
    }
}

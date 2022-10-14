using FMFT.Web.Server.Brokers.Loggings;
using FMFT.Web.Server.Models.Accounts;
using FMFT.Web.Server.Models.Accounts.Params;
using FMFT.Web.Server.Models.UserAccounts;
using FMFT.Web.Server.Models.UserAccounts.Exceptions;
using FMFT.Web.Server.Models.UserAccounts.Requests;
using FMFT.Web.Server.Models.Users;
using FMFT.Web.Server.Models.Users.Params;
using FMFT.Web.Server.Services.Processings.Accounts;
using FMFT.Web.Server.Services.Processings.Users;
using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Server.Services.Orchestrations.UserAccounts
{
    public partial class UserAccountOrchestrationService : IUserAccountOrchestrationService
    {
        private readonly IAccountProcessingService accountService;
        private readonly IUserProcessingService userService;
        private readonly ILoggingBroker loggingBroker;

        public UserAccountOrchestrationService(
            IAccountProcessingService accountService,
            IUserProcessingService userService,
            ILoggingBroker loggingBroker)
        {
            this.accountService = accountService;
            this.userService = userService;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<IEnumerable<User>> RetrieveAllUsersAsync()
            => TryCatch(async () =>
            {
                await AuthorizeUserAccountByRolePrivateAsync(UserRole.Admin);
                return await userService.RetrieveAllUsersAsync();
            });

        public ValueTask<User> RetrieveUserByIdAsync(int userId)
            => TryCatch(async () =>
            {
                await AuthorizeUserAccountByUserIdOrRolesPrivateAsync(userId, UserRole.Admin);
                return await userService.RetrieveUserByIdAsync(userId);
            });

        public ValueTask<AccountToken> RegisterWithPasswordAsync(RegisterWithPasswordRequest request)
            => TryCatch(async () =>
            {
                RegisterUserWithPasswordProcessingParams args = new()
                {
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    PasswordText = request.PasswordText
                };

                User user = await userService.RegisterUserWithPasswordAsync(args);
                Account account = MapUserToAccount(user);

                CreateTokenParams @params = new()
                {
                    Account = account,
                    AuthenticationMethod = null
                };

                return await accountService.CreateTokenAsync(@params);
            });

        public ValueTask<AccountToken> SignInWithPasswordAsync(SignInWithPasswordRequest request)
            => TryCatch(async () =>
            {
                User user = await userService.RetrieveUserByEmailAndPasswordAsync(request.Email, request.PasswordText);
                Account account = MapUserToAccount(user);

                CreateTokenParams @params = new()
                {
                    Account = account,
                    AuthenticationMethod = null,
                };

                return await accountService.CreateTokenAsync(@params);
            });

        public ValueTask UpdateUserRoleAsync(UpdateUserRoleParams @params)
            => TryCatch(async () =>
            {
                await AuthorizeUserAccountByRolePrivateAsync(UserRole.Admin);
                await userService.UpdateUserRoleAsync(@params);
            });

        public ValueTask UpdateUserCultureAsync(UpdateUserCultureParams @params)
            => TryCatch(async () =>
            {
                await accountService.AuthorizeAccountByUserIdAsync(@params.UserId);
                await userService.UpdateUserCultureAsync(@params);
            });

        public ValueTask<UserAccount> RetrieveUserAccountAsync()
            => TryCatch(async () =>
            {
                return await RetrieveUserAccountPrivateAsync();
            });

        private async ValueTask<UserAccount> RetrieveUserAccountPrivateAsync()
        {
            Account account = await accountService.RetrieveAccountAsync();
            User user = await userService.RetrieveUserByIdAsync(account.UserId);
            UserAccount userAccount = MapUserToUserAccount(user);
            return userAccount;
        }

        public ValueTask AuthorizeAccountAsync()
            => TryCatch(async () =>
            {
                await accountService.AuthorizeAccountAsync();
            });

        public ValueTask AuthorizeAccountByUserIdAsync(int userId)
            => TryCatch(async () =>
            {
                await accountService.AuthorizeAccountByUserIdAsync(userId);
            });

        public ValueTask AuthorizeUserAccountByRoleAsync(params UserRole[] authorizedRoles)
            => TryCatch(async () =>
            {
                await AuthorizeUserAccountByRolePrivateAsync(authorizedRoles);
            });

        private async ValueTask AuthorizeUserAccountByRolePrivateAsync(params UserRole[] authorizedRoles)
        {
            UserAccount userAccount = await RetrieveUserAccountPrivateAsync();

            if (!authorizedRoles.Contains(userAccount.Role))
            {
                throw new NotAuthorizedUserAccountOrchestrationException();
            }
        }

        public ValueTask AuthorizeUserAccountByUserIdOrRolesAsync(int userId, params UserRole[] authorizedRoles)
        => TryCatch(async () =>
        {
            await AuthorizeUserAccountByUserIdOrRolesPrivateAsync(userId, authorizedRoles);
        });

        private async Task AuthorizeUserAccountByUserIdOrRolesPrivateAsync(int userId, params UserRole[] authorizedRoles)
        {
            UserAccount userAccount = await RetrieveUserAccountPrivateAsync();

            if (userAccount.UserId != userId && !authorizedRoles.Contains(userAccount.Role))
            {
                throw new NotAuthorizedUserAccountOrchestrationException();
            }
        }

        public ValueTask<Account> RetrieveAccountAsync()
            => TryCatch(async () =>
            {
                return await accountService.RetrieveAccountAsync();
            });
    }
}

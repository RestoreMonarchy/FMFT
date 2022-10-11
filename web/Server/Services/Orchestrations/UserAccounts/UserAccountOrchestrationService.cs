using FMFT.Web.Server.Brokers.Loggings;
using FMFT.Web.Server.Models.Accounts;
using FMFT.Web.Server.Models.Accounts.Exceptions;
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
                await AuthorizeUserAccountByRoleAsync(UserRole.Admin);
                return await userService.RetrieveAllUsersAsync();
            });

        public ValueTask<User> RetrieveUserByIdAsync(int userId)
            => TryCatch(async () =>
            {
                await AuthorizeUserAccountByUserIdOrRolesAsync(userId, UserRole.Admin);
                return await userService.RetrieveUserByIdAsync(userId);
            });

        public ValueTask<string> RegisterWithPasswordAsync(RegisterWithPasswordRequest request)
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
                    AuthenticationMethod = null,
                    IsPersistent = false
                };

                return await accountService.CreateTokenAsync(@params);
            });

        public ValueTask<string> SignInWithPasswordAsync(SignInWithPasswordRequest request)
            => TryCatch(async () =>
            {
                User user = await userService.RetrieveUserByEmailAndPasswordAsync(request.Email, request.PasswordText);
                Account account = MapUserToAccount(user);

                CreateTokenParams @params = new()
                {
                    Account = account,
                    AuthenticationMethod = null,
                    IsPersistent = request.IsPersistent
                };

                return await accountService.CreateTokenAsync(@params);
            });

        public ValueTask UpdateUserRoleAsync(UpdateUserRoleParams @params)
            => TryCatch(async () =>
            {
                await AuthorizeUserAccountByRoleAsync(UserRole.Admin);
                await userService.UpdateUserRoleAsync(@params);
            });

        public ValueTask UpdateUserCultureAsync(UpdateUserCultureParams @params)
            => TryCatch(async () =>
            {
                await AuthorizeAccountByUserIdAsync(@params.UserId);
                await userService.UpdateUserCultureAsync(@params);
            });

        public ValueTask<UserAccount> RetrieveUserAccountAsync()
            => TryCatch(async () =>
            {
                UserAccount userAccount = new();
                userAccount.Account = await RetrieveAccountAsync();
                userAccount.User = await RetrieveUserByIdAsync(userAccount.Account.UserId);
                return userAccount;
            });

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
                UserAccount userAccount = await RetrieveUserAccountAsync();

                if (!authorizedRoles.Contains(userAccount.User.Role))
                {
                    throw new NotAuthorizedUserAccountOrchestrationException();
                }
            });

        public ValueTask AuthorizeUserAccountByUserIdOrRolesAsync(int userId, params UserRole[] authorizedRoles)
        => TryCatch(async () =>
        {
            UserAccount userAccount = await RetrieveUserAccountAsync();

            if (userAccount.User.Id != userId && !authorizedRoles.Contains(userAccount.User.Role))
            {
                throw new NotAuthorizedUserAccountOrchestrationException();
            }
        });

        public ValueTask<Account> RetrieveAccountAsync()
            => TryCatch(async () =>
            {
                return await accountService.RetrieveAccountAsync();
            });
    }
}

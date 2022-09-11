using FMFT.Web.Server.Services.Processings.Accounts;
using FMFT.Web.Server.Services.Processings.Users;
using FMFT.Web.Server.Models.Accounts;
using FMFT.Web.Server.Models.Accounts.Arguments;
using FMFT.Web.Server.Models.Accounts.Params;
using FMFT.Web.Shared.Enums;
using FMFT.Web.Server.Models.UserAccounts.Requests;
using FMFT.Web.Server.Models.Users;
using FMFT.Web.Server.Models.Users.Arguments;
using FMFT.Web.Server.Models.Users.Exceptions;
using FMFT.Web.Server.Models.Users.Params;

namespace FMFT.Web.Server.Services.Orchestrations.UserAccounts
{
    public partial class UserAccountOrchestrationService : IUserAccountOrchestrationService
    {
        private readonly IAccountProcessingService accountService;
        private readonly IUserProcessingService userService;

        public UserAccountOrchestrationService(IAccountProcessingService accountService, IUserProcessingService userService)
        {
            this.accountService = accountService;
            this.userService = userService;
        }

        public async ValueTask<IEnumerable<User>> RetrieveAllUsersAsync()
        {
            accountService.AuthorizeAccountByRole(UserRole.Admin);
            return await userService.RetrieveAllUsersAsync();
        }

        public async ValueTask<User> RetrieveUserByIdAsync(int userId)
        {
            accountService.AuthorizeAccountByUserIdOrRoles(userId, UserRole.Admin);
            return await userService.RetrieveUserByIdAsync(userId);
        }

        public async ValueTask<Account> RegisterWithPasswordAsync(RegisterWithPasswordRequest request)
        {
            RegisterUserWithPasswordArguments args = new() 
            { 
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PasswordText = request.PasswordText
            };

            User user = await userService.RegisterUserWithPasswordAsync(args);
            Account account = MapUserToAccount(user);

            SignInAccountParams @params = new()
            {
                Account = account,
                AuthenticationMethod = null,
                IsPersistent = false
            };

            await accountService.SignInAccountAsync(@params);
            return account;
        }

        public async ValueTask<Account> SignInWithPasswordAsync(SignInWithPasswordRequest request)
        {
            User user = await userService.RetrieveUserByEmailAndPasswordAsync(request.Email, request.PasswordText);
            Account account = MapUserToAccount(user);

            SignInAccountParams @params = new()
            {
                Account = account,
                AuthenticationMethod = null,
                IsPersistent = request.IsPersistent
            };

            await accountService.SignInAccountAsync(@params);
            return account;
        }

        public async ValueTask SignOutAsync()
        {
            await accountService.SignOutAccountAsync();
        }

        public async ValueTask<Account> HandleExternalLoginCallbackAsync()
        {
            ExternalLogin externalLogin = await accountService.RetrieveExternalLoginAsync();

            User user;
            try
            {
                user = await userService.RetrieveUserByLoginAsync(externalLogin.ProviderName, externalLogin.ProviderKey);
            } catch (UserNotFoundException)
            {
                RegisterUserWithLoginParams registerParams = new()
                {
                    Email = externalLogin.Account.Email,
                    FirstName = externalLogin.Account.FirstName,
                    LastName = externalLogin.Account.LastName,
                    Role = UserRole.Guest,
                    IsEmailConfirmed = true,
                    ProviderKey = externalLogin.ProviderKey,
                    ProviderName = externalLogin.ProviderName
                };

                user = await userService.RegisterUserWithLoginAsync(registerParams);
            }

            Account account = MapUserToAccount(user);
            SignInAccountParams signinParams = new()
            {
                Account = account,
                AuthenticationMethod = externalLogin.ProviderName,
                IsPersistent = true
            };
            await accountService.SignInAccountAsync(signinParams);
            return account;
        }

        public async ValueTask ChallengeExternalLoginAsync(string provider, string returnUrl)
        {
            ChallengeExternalLoginArguments arguments = new()
            {
                Provider = provider,
                ReturnUrl = returnUrl
            };
            await accountService.ChallengeExternalLoginAsync(arguments);
        }

        public async ValueTask<Account> ConfirmExternalLoginAsync(ConfirmExternalLoginRequest request)
        {
            ExternalLogin externalLogin = await accountService.RetrieveExternalLoginAsync();

            RegisterUserWithLoginParams registerParams = new()
            {
                Email = request.Email,
                FirstName = externalLogin.Account.FirstName,
                LastName = externalLogin.Account.LastName,
                Role = UserRole.Guest,
                IsEmailConfirmed = false,
                ProviderKey = externalLogin.ProviderKey,
                ProviderName = externalLogin.ProviderName
            };

            User user = await userService.RegisterUserWithLoginAsync(registerParams);
            Account account = MapUserToAccount(user);

            SignInAccountParams signinParams = new()
            {
                Account = account,
                AuthenticationMethod = externalLogin.ProviderName,
                IsPersistent = true
            };

            await accountService.SignInAccountAsync(signinParams);
            return account;
        }

        public async ValueTask UpdateUserRoleAsync(UpdateUserRoleParams @params)
        {
            accountService.AuthorizeAccountByRole(UserRole.Admin);
            await userService.UpdateUserRoleAsync(@params);
        }

        public async ValueTask UpdateUserCultureAsync(UpdateUserCultureParams @params)
        {
            accountService.AuthorizeAccountByUserId(@params.UserId);
            await userService.UpdateUserCultureAsync(@params);
        }

        public Account RetrieveAccount()
        {
            return accountService.RetrieveAccount();
        }
    }
}

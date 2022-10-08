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
using FMFT.Web.Server.Brokers.Loggings;

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
                await accountService.AuthorizeAccountByRoleAsync(UserRole.Admin);
                return await userService.RetrieveAllUsersAsync();
            });

        public ValueTask<User> RetrieveUserByIdAsync(int userId)
            => TryCatch(async () =>
            {
                await accountService.AuthorizeAccountByUserIdOrRolesAsync(userId, UserRole.Admin);
                return await userService.RetrieveUserByIdAsync(userId);
            });

        public ValueTask<Account> RegisterWithPasswordAsync(RegisterWithPasswordRequest request)
            => TryCatch(async () =>
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
            });

        public ValueTask<Account> SignInWithPasswordAsync(SignInWithPasswordRequest request)
            => TryCatch(async () =>
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
            });

        public ValueTask SignOutAsync()
            => TryCatch(async () =>
            {
                await accountService.SignOutAccountAsync();
            });

        public ValueTask<Account> HandleExternalLoginCallbackAsync()
            => TryCatch(async () =>
            {
                ExternalLogin externalLogin = await accountService.RetrieveExternalLoginAsync();

                User user;
                try
                {
                    user = await userService.RetrieveUserByLoginAsync(externalLogin.ProviderName, externalLogin.ProviderKey);
                }
                catch (UserProcessingDependencyValidationException exception) when (exception.InnerException is NotFoundUserException)
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
            });

        public ValueTask ChallengeExternalLoginAsync(string provider, string returnUrl)
            => TryCatch(async () => 
            {
                ChallengeExternalLoginArguments arguments = new()
                {
                    Provider = provider,
                    ReturnUrl = returnUrl
                };
                await accountService.ChallengeExternalLoginAsync(arguments);
            });

        public ValueTask<Account> ConfirmExternalLoginAsync(ConfirmExternalLoginRequest request)
            => TryCatch(async () =>
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
            });

        public ValueTask UpdateUserRoleAsync(UpdateUserRoleParams @params)
            => TryCatch(async () =>
            {
                await accountService.AuthorizeAccountByRoleAsync(UserRole.Admin);
                await userService.UpdateUserRoleAsync(@params);
            });

        public ValueTask UpdateUserCultureAsync(UpdateUserCultureParams @params)
            => TryCatch(async () =>
            {
                await accountService.AuthorizeAccountByUserIdAsync(@params.UserId);
                await userService.UpdateUserCultureAsync(@params);
            });

        public ValueTask<Account> RetrieveAccountAsync()
            => TryCatch(async () =>
            {
                return await accountService.RetrieveAccountAsync();
            });
    }
}

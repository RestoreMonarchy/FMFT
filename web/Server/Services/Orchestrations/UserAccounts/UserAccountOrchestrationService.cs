using FMFT.Web.Server.Services.Processings.Accounts;
using FMFT.Web.Server.Services.Processings.Users;
using FMFT.Web.Shared.Models.Accounts;
using FMFT.Web.Shared.Models.Accounts.Params;
using FMFT.Web.Shared.Models.UserAccounts.Requests;
using FMFT.Web.Shared.Models.Users;
using FMFT.Web.Shared.Models.Users.Arguments;
using FMFT.Web.Shared.Models.Users.Exceptions;
using FMFT.Web.Shared.Models.Users.Params;

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

        public async ValueTask<Account> HandleExternalLoginCallbackAsync()
        {
            ExternalLogin externalLogin = await accountService.RetrieveExternalLoginAsync();

            User user;
            try
            {
                user = await userService.RetrieveUserByLoginAsync(externalLogin.ProviderName, externalLogin.ProviderKey);
            } catch (UserNotFoundException)
            {
                RegisterUserWithLoginParams @params = new()
                {
                    Email = externalLogin.Account.Email,
                    FirstName = externalLogin.Account.FirstName,
                    LastName = externalLogin.Account.LastName,
                    Role = UserRole.Guest,
                    IsEmailConfirmed = true,
                    ProviderKey = externalLogin.ProviderKey,
                    ProviderName = externalLogin.ProviderName
                };

                user = await userService.RegisterUserWithLoginAsync(@params);
            }

            Account account = MapUserToAccount(user);
            SignInAccountParams @signinAccountParams = new SignInAccountParams()
            {
                Account = account,
                AuthenticationMethod = externalLogin.ProviderName,
                IsPersistent = true
            };
            await accountService.SignInAccountAsync(signinAccountParams);
            return account;
        }

        public async ValueTask ChallengeExternalLoginAsync(string provider, string returnUrl)
        {
            ChallengeExternalLoginParams @params = new()
            {
                Provider = provider,
                RedirectUrl = returnUrl
            };
            await accountService.ChallengeExternalLoginAsync(@params);
        }
    }
}

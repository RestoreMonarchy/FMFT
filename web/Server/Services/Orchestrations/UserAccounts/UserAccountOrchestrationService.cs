using FMFT.Emails.Server.Models;
using FMFT.Web.Server.Brokers.Loggings;
using FMFT.Web.Server.Models.Accounts;
using FMFT.Web.Server.Models.Accounts.Params;
using FMFT.Web.Server.Models.Emails.Params;
using FMFT.Web.Server.Models.Facebooks;
using FMFT.Web.Server.Models.Googles;
using FMFT.Web.Server.Models.UserAccounts;
using FMFT.Web.Server.Models.UserAccounts.Exceptions;
using FMFT.Web.Server.Models.UserAccounts.Requests;
using FMFT.Web.Server.Models.Users;
using FMFT.Web.Server.Models.Users.Exceptions;
using FMFT.Web.Server.Models.Users.Params;
using FMFT.Web.Server.Models.Users.Requests;
using FMFT.Web.Server.Services.Foundations.Accounts;
using FMFT.Web.Server.Services.Foundations.Emails;
using FMFT.Web.Server.Services.Foundations.Facebooks;
using FMFT.Web.Server.Services.Foundations.Googles;
using FMFT.Web.Server.Services.Foundations.Users;
using FMFT.Web.Shared.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FMFT.Web.Server.Services.Orchestrations.UserAccounts
{
    public partial class UserAccountOrchestrationService : IUserAccountOrchestrationService
    {
        private readonly IAccountService accountService;
        private readonly IUserService userService;
        private readonly IEmailService emailService;
        private readonly IFacebookService facebookService;
        private readonly IGoogleService googleService;

        public UserAccountOrchestrationService(
            IAccountService accountService,
            IUserService userService,
            IEmailService emailService,
            IFacebookService facebookService,
            IGoogleService googleService)
        {
            this.accountService = accountService;
            this.userService = userService;
            this.emailService = emailService;
            this.facebookService = facebookService;
            this.googleService = googleService;
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

        public async ValueTask ConfirmEmailAsync(int userId, Guid secretKey)
        {
            await userService.ConfirmEmailAsync(userId, secretKey);
        }

        public async ValueTask SendUserConfirmAccountEmailAsync(int userId)
        {
            await AuthorizeAccountByUserIdAsync(userId);

            User user = await userService.RetrieveUserByIdAsync(userId);

            if (user.IsEmailConfirmed)
            {
                throw new AlreadyConfirmedEmailUserException();
            }

            if (user.ConfirmEmailSendDate < DateTimeOffset.Now.AddMinutes(5))
            {
                throw new LimitConfirmEmailUserAccountException();
            }

            await userService.UpdateConfirmEmailSendDateAsync(user.Id);

            ConfirmAccountEmailParams @params = new()
            {
                FirstName = user.FirstName,
                UserId = user.Id,
                ConfirmSecret = user.ConfirmEmailSecret
            };

            await emailService.EnqueueSendConfirmAccountEmailAsync(user.Email, @params);
        }

        public async ValueTask<AccountToken> RegisterWithPasswordAsync(RegisterUserWithPasswordRequest request)
        {
            User user = await userService.RegisterUserWithPasswordAsync(request);

            RegisterEmailParams @registerEmailParams = new()
            {
                UserId = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                ConfirmSecret = user.ConfirmEmailSecret
            };

            await emailService.EnqueueSendRegisterEmailAsync(user.Email, @registerEmailParams);

            Account account = MapUserToAccount(user);
            CreateTokenParams @createTokenParams = new()
            {
                Account = account
            };

            return await accountService.CreateTokenAsync(@createTokenParams);
        }

        public async ValueTask<AccountToken> LoginWithFacebookAsync(LoginWithFacebookRequest request)
        {
            FacebookUser facebookUser = await facebookService.GetFacebookUserAsync(request.AccessToken);

            User user;
            try
            {
                user = await userService.RetrieveUserByLoginAsync("Facebook", facebookUser.Id);
            } catch (NotFoundUserException)
            {
                user = await RegisterFacebookUserAsync(facebookUser);
            }

            CreateTokenParams @createTokenParams = new()
            {
                Account = MapUserToAccount(user)
            };

            return await accountService.CreateTokenAsync(createTokenParams);
        }

        public async ValueTask<AccountToken> LoginWithGoogleAsync(LoginWithGoogleRequest request)
        {
            GoogleUser googleUser = await googleService.GetGoogleUserAsync(request.Credential);

            User user;
            try
            {
                user = await userService.RetrieveUserByLoginAsync("Google", googleUser.Id);
            }
            catch (NotFoundUserException)
            {
                user = await RegisterGoogleUserAsync(googleUser);
            }

            CreateTokenParams @createTokenParams = new()
            {
                Account = MapUserToAccount(user)
            };

            return await accountService.CreateTokenAsync(createTokenParams);
        }

        private async ValueTask<User> RegisterGoogleUserAsync(GoogleUser googleUser)
        {
            RegisterUserWithLoginParams @registerUserWithLoginParams = new()
            {
                Email = googleUser.Email,
                FirstName = googleUser.FirstName,
                LastName = googleUser.LastName,
                Role = UserRole.Guest,
                IsEmailConfirmed = true,
                ProviderName = "Google",
                ProviderKey = googleUser.Id,
                FriendlyName = googleUser.Email
            };

            User user = await userService.RegisterUserWithLoginAsync(@registerUserWithLoginParams);

            RegisterExternalEmailParams @registerExternalEmailParams = new()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                AuthenticationMethod = "Google"
            };

            await emailService.EnqueueSendRegisterExternalEmailAsync(user.Email, @registerExternalEmailParams);

            return user;
        }

        private async ValueTask<User> RegisterFacebookUserAsync(FacebookUser facebookUser)
        {
            RegisterUserWithLoginParams @registerUserWithLoginParams = new()
            {
                Email = facebookUser.Email,
                FirstName = facebookUser.FirstName,
                LastName = facebookUser.LastName,
                Role = UserRole.Guest,
                IsEmailConfirmed = true,
                ProviderName = "Facebook",
                ProviderKey = facebookUser.Id,
                FriendlyName = facebookUser.Email
            };

            User user = await userService.RegisterUserWithLoginAsync(@registerUserWithLoginParams);

            RegisterExternalEmailParams @registerExternalEmailParams = new()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                AuthenticationMethod = "Facebook"
            };

            await emailService.EnqueueSendRegisterExternalEmailAsync(user.Email, @registerExternalEmailParams);

            return user;
        }

        public async ValueTask<AccountToken> SignInWithPasswordAsync(SignInWithPasswordRequest request)
        {
            User user = await userService.RetrieveUserByEmailAndPasswordAsync(request.Email, request.PasswordText);
            Account account = MapUserToAccount(user);

            CreateTokenParams @params = new()
            {
                Account = account
            };

            return await accountService.CreateTokenAsync(@params);
        }

        public async ValueTask<IEnumerable<UserLogin>> RetrieveUserLoginsByUserIdAsync(int userId)
        {
            await AuthorizeUserAccountByUserIdOrRolesAsync(userId, UserRole.Admin);

            return await userService.RetrieveUserLoginsByUserIdAsync(userId);
        }

        public async ValueTask ChangeUserAccountPasswordAsync(ChangeUserAccountPasswordRequest request)
        {
            Account account = await accountService.RetrieveAccountAsync();

            ChangeUserPasswordRequest changeUserPasswordRequest = new()
            {
                UserId = account.UserId,
                CurrentPasswordText = request.CurrentPasswordText,
                PasswordText = request.PasswordText
            };

            await userService.ChangeUserPasswordAsync(changeUserPasswordRequest);
        }

        public async ValueTask UpdateUserPasswordAsync(UpdateUserPasswordRequest request)
        {
            await AuthorizeUserAccountByRoleAsync(UserRole.Admin);
            await userService.UpdateUserPasswordAsync(request);
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

        public async ValueTask AuthorizeUserAccountByConfirmedEmailAsync()
        {
            UserAccount userAccount = await RetrieveUserAccountAsync();

            if (!userAccount.IsEmailConfirmed)
            {
                throw new NotConfirmedEmailUserAccountException();
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

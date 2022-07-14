using FMFT.Extensions.Authentication.Models;
using FMFT.Extensions.Authentication.Models.Exceptions;
using FMFT.Web.Server.Brokers.Authentications;
using FMFT.Web.Server.Brokers.Encryptions;
using FMFT.Web.Server.Brokers.Urls;
using FMFT.Web.Server.Brokers.Validations;
using FMFT.Web.Server.Services.Foundations.Users;
using FMFT.Web.Shared.Models.Users;
using FMFT.Web.Shared.Models.Users.Exceptions;
using FMFT.Web.Shared.Models.Users.Models;
using FMFT.Web.Shared.Models.Users.Params;
using System.Security.Claims;

namespace FMFT.Web.Server.Services.Processings.Users
{
    public partial class UserProcessingService : IUserProcessingService
    {
        private readonly IUserService userService;
        private readonly IAuthenticationBroker authenticationBroker;
        private readonly IEncryptionBroker encryptionBroker;
        private readonly IValidationBroker validationBroker;
        private readonly IUrlBroker urlBroker;

        public UserProcessingService(
            IUserService userService,
            IAuthenticationBroker authenticationBroker,
            IEncryptionBroker encryptionBroker,
            IValidationBroker validationBroker,
            IUrlBroker urlBroker)
        {
            this.userService = userService;
            this.authenticationBroker = authenticationBroker;
            this.encryptionBroker = encryptionBroker;
            this.validationBroker = validationBroker;
            this.urlBroker = urlBroker;
        }

        public async ValueTask<User> GetAuthenticatedUserAsync()
        {
            int userId;
            try
            {
                userId = authenticationBroker.AuthenticatedUserId;
            } catch (NotAuthenticatedException)
            {
                throw new UserNotAuthenticatedException();
            }
            
            return await userService.RetrieveUserByIdAsync(userId);
        }

        public async ValueTask<UserInfo> GetAuthenticatedUserInfoAsync()
        {
            User user = await GetAuthenticatedUserAsync();
            return MapUserToUserInfo(user);
        }

        public async ValueTask SignOutUserAsync()
        {
            await authenticationBroker.SignOutAsync();
        }

        public async ValueTask<UserInfo> RegisterUserWithPasswordAsync(RegisterUserWithPasswordModel model)
        {
            if (validationBroker.IsPasswordInvalid(model.PasswordText))
            {
                throw new UserPasswordInvalidException();
            }

            RegisterUserWithPasswordParams @params = new()
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PasswordHash = encryptionBroker.HashPassword(model.PasswordText),
                Role = UserRole.Guest
            };

            User user = await userService.RegisterUserWithPasswordAsync(@params);
            await SignInUserAsync(user, false, null);
            return MapUserToUserInfo(user);
        }

        public async ValueTask<UserInfo> SignInUserWithPasswordAsync(SignInUserWithPasswordModel model)
        {
            User user;
            try
            {
                user = await userService.RetrieveUserByEmailAsync(model.Email);
            } catch (UserNotFoundException)
            {
                throw new UserPasswordNotMatchException();
            }            
            
            if (!encryptionBroker.VerifyPassword(model.PasswordText, user.PasswordHash))
            {
                throw new UserPasswordNotMatchException();
            }

            await SignInUserAsync(user, model.IsPersistent, null);
            return MapUserToUserInfo(user);
        }

        public async ValueTask ChallengeExternalLoginAsync(string provider, string returnUrl)
        {
            string redirectUrl = urlBroker.Action("ExternalLoginCallback", "Account", new { returnUrl });
            await authenticationBroker.ChallengeExternalLoginAsync(provider, redirectUrl);
        }

        public async ValueTask HandleExternalLoginCallbackAsync()
        {
            ExternalLoginInfo externalLoginInfo = await authenticationBroker.GetExternalLoginInfoAsync();
            if (externalLoginInfo == null)
            {
                throw new ExternalLoginInfoNotFoundException();
            }

            try
            {
                await SignInUserWithExternalLoginAsync(externalLoginInfo);
            } catch (UserNotFoundException)
            {
                await RegisterUserWithExteranlLoginAsync(externalLoginInfo);
            }
        }

        public async ValueTask<UserInfo> ConfirmExternalLoginAsync(ExternalLoginConfirmationModel model)
        {
            ExternalLoginInfo externalLoginInfo = await authenticationBroker.GetExternalLoginInfoAsync();
            if (externalLoginInfo == null)
            {
                throw new ExternalLoginInfoNotFoundException();
            }

            RegisterUserWithLoginParams @params = MapExternalLoginInfoAndConfirmationToRegisterUserWithLoginParams(externalLoginInfo, model);
            User user = await userService.RegisterUserWithLoginAsync(@params);
            await SignInUserAsync(user, true, externalLoginInfo.ProviderName);
            return MapUserToUserInfo(user);
        }

        private async ValueTask SignInUserWithExternalLoginAsync(ExternalLoginInfo externalLoginInfo)
        {
            User user = await userService.RetrieveUserByLoginAsync(externalLoginInfo.ProviderName, externalLoginInfo.ProviderKey);
            await SignInUserAsync(user, true, externalLoginInfo.ProviderName);
        }

        private async ValueTask RegisterUserWithExteranlLoginAsync(ExternalLoginInfo externalLoginInfo)
        {
            RegisterUserWithLoginParams @params = MapExternalLoginInfoToRegisterUserWithLoginParams(externalLoginInfo);
            User user = await userService.RegisterUserWithLoginAsync(@params);
            await SignInUserAsync(user, true, externalLoginInfo.ProviderName);
        }

        private async ValueTask SignInUserAsync(User user, bool isPersistent, string authenticationMethod)
        {
            Dictionary<string, object> claimsDictionary = MapUserToClaimsDictionary(user);
            await authenticationBroker.SignInAsync(claimsDictionary, isPersistent, authenticationMethod);
        }
    }
}

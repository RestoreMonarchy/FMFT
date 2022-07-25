using FMFT.Web.Shared.Models.Users.Exceptions;
using FMFT.Web.Shared.Models.Users.Models;
using FMFT.Web.Shared.Models.Users.Params;
using FMFT.Web.Shared.Models.Users;
using FMFT.Extensions.Authentication.Models;

namespace FMFT.Web.Server.Services.Processings.Users
{
    public partial class UserProcessingService
    {
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
            }
            catch (UserNotFoundException)
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
            }
            catch (UserNotFoundException)
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

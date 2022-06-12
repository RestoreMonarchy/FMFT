using FMFT.Web.Server.Brokers.Authentications;
using FMFT.Web.Server.Brokers.Encryptions;
using FMFT.Web.Server.Brokers.Validations;
using FMFT.Web.Server.Services.Foundations.Users;
using FMFT.Web.Shared.Models.Users;
using FMFT.Web.Shared.Models.Users.Exceptions;
using FMFT.Web.Shared.Models.Users.Models;
using FMFT.Web.Shared.Models.Users.Params;
using System.Security.Claims;

namespace FMFT.Web.Server.Services.Processings.Users
{
    public class UserProcessingService : IUserProcessingService
    {
        private readonly IUserService userService;
        private readonly IAuthenticationBroker authenticationBroker;
        private readonly IEncryptionBroker encryptionBroker;
        private readonly IValidationBroker validationBroker;

        public UserProcessingService(
            IUserService userService, 
            IAuthenticationBroker authenticationBroker, 
            IEncryptionBroker encryptionBroker,
            IValidationBroker validationBroker)
        {
            this.userService = userService;
            this.authenticationBroker = authenticationBroker;
            this.encryptionBroker = encryptionBroker;
            this.validationBroker = validationBroker;
        }

        public async ValueTask SignOutUserAsync()
        {
            await authenticationBroker.SignOutAsync();
        }

        public async ValueTask RegisterUserWithPasswordAsync(RegisterUserWithPasswordModel model)
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
        }

        public async ValueTask SignInUserWithPasswordAsync(SignInUserWithPasswordModel model)
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
        }

        private async ValueTask SignInUserAsync(User user, bool isPersistent, string authenticationMethod)
        {
            Dictionary<string, object> claimsDictionary = UserToClaimsDictionary(user);
            await authenticationBroker.SignInAsync(claimsDictionary, isPersistent, authenticationMethod);
        }

        private Dictionary<string, object> UserToClaimsDictionary(User user)
        {
            return new Dictionary<string, object>()
            {
                { ClaimTypes.NameIdentifier, user.Id },
                { ClaimTypes.Email, user.Email },
                { ClaimTypes.GivenName, user.FirstName },
                { ClaimTypes.Surname, user.LastName },
                { ClaimTypes.Role, user.Role }             
            };
        }
    }
}

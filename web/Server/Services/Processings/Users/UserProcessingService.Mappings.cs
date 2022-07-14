using FMFT.Extensions.Authentication.Models;
using FMFT.Web.Shared.Models.Users;
using FMFT.Web.Shared.Models.Users.Params;
using System.Security.Claims;

namespace FMFT.Web.Server.Services.Processings.Users
{
    public partial class UserProcessingService
    {
        private RegisterUserWithLoginParams MapExternalLoginInfoToRegisterUserWithLoginParams(ExternalLoginInfo externalLoginInfo)
        {
            return new RegisterUserWithLoginParams()
            {
                Email = externalLoginInfo.Principal.FindFirstValue(ClaimTypes.Email),
                FirstName = externalLoginInfo.Principal.FindFirstValue(ClaimTypes.GivenName),
                LastName = externalLoginInfo.Principal.FindFirstValue(ClaimTypes.Surname),
                ProviderName = externalLoginInfo.ProviderName,
                ProviderKey = externalLoginInfo.ProviderKey,
                Role = UserRole.Guest,
                IsEmailConfirmed = true
            };
        }

        private Dictionary<string, object> MapUserToClaimsDictionary(User user)
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

        private UserInfo MapUserToUserInfo(User user)
        {
            return new UserInfo()
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role
            };
        }
    }
}

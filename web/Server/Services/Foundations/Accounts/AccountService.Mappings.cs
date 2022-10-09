using FMFT.Extensions.Authentication.Models;
using FMFT.Web.Server.Models.Accounts;
using FMFT.Web.Shared.Enums;
using System.Security.Claims;

namespace FMFT.Web.Server.Services.Foundations.Accounts
{
    public partial class AccountService
    {
        private Dictionary<string, object> MapAccountToDictionary(Account account)
        {
            return new()
            {
                { "userId", account.UserId },
                { "name", account.Name },
                { "email", account.Email },
                { "firstName", account.FirstName },
                { "lastName", account.LastName },
                { "role", account.Role }
            };
        }

        private Account MapClaimsPrincipalToAccount(ClaimsPrincipal claimsPrincipal)
        {
            return new Account()
            {
                UserId = int.Parse(claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier)),
                Name = claimsPrincipal.FindFirstValue(ClaimTypes.Name),
                Email = claimsPrincipal.FindFirstValue(ClaimTypes.Email),
                FirstName = claimsPrincipal.FindFirstValue(ClaimTypes.GivenName),
                LastName = claimsPrincipal.FindFirstValue(ClaimTypes.Surname),
                Role = Enum.Parse<UserRole>(claimsPrincipal.FindFirstValue(ClaimTypes.Role)),
                AuthenticationMethod = claimsPrincipal.FindFirstValue(ClaimTypes.AuthenticationMethod)
            };
        }

        private ExternalAccount MapClaimsPrincipalToExternalAccount(ClaimsPrincipal claimsPrincipal)
        {
            return new ExternalAccount()
            {
                ProviderKey = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier),
                Name = claimsPrincipal.FindFirstValue(ClaimTypes.Name),
                Email = claimsPrincipal.FindFirstValue(ClaimTypes.Email),
                FirstName = claimsPrincipal.FindFirstValue(ClaimTypes.GivenName),
                LastName = claimsPrincipal.FindFirstValue(ClaimTypes.Surname)
            };
        }

        private ExternalLogin MapExternalLoginInfoToExternalLogin(ExternalLoginInfo externalLoginInfo)
        {
            return new ExternalLogin()
            {
                Account = MapClaimsPrincipalToExternalAccount(externalLoginInfo.Principal),
                ProviderName = externalLoginInfo.ProviderName,
                ProviderKey = externalLoginInfo.ProviderKey
            };
        }
    }
}

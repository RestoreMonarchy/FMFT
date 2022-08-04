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
                { ClaimTypes.NameIdentifier, account.UserId },
                { ClaimTypes.Name, account.Name },
                { ClaimTypes.Email, account.Email },
                { ClaimTypes.GivenName, account.FirstName },
                { ClaimTypes.Surname, account.LastName },
                { ClaimTypes.Role, account.Role }
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
                Role = Enum.Parse<UserRole>(claimsPrincipal.FindFirstValue(ClaimTypes.Role))
            };
        }

        private ExternalLogin MapExternalLoginInfoToExternalLogin(ExternalLoginInfo externalLoginInfo)
        {
            return new ExternalLogin()
            {
                Account = MapClaimsPrincipalToAccount(externalLoginInfo.Principal),
                ProviderName = externalLoginInfo.ProviderName,
                ProviderKey = externalLoginInfo.ProviderKey
            };
        }
    }
}

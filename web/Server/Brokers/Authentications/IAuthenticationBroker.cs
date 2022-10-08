using FMFT.Extensions.Authentication.Models;
using System.Security.Claims;

namespace FMFT.Web.Server.Brokers.Authentications
{
    public interface IAuthenticationBroker
    {
        string CreateToken(Dictionary<string, object> claimsDict);
        ClaimsPrincipal GetClaimsPrincipal();
    }
}
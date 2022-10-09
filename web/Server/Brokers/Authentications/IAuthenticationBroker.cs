using FMFT.Extensions.Authentication.Models;
using System.Security.Claims;

namespace FMFT.Web.Server.Brokers.Authentications
{
    public interface IAuthenticationBroker
    {
        string CreateToken<T>(T payload);
        ClaimsPrincipal GetClaimsPrincipal();
    }
}
using FMFT.Extensions.Authentication.Models;
using System.Security.Claims;

namespace FMFT.Web.Server.Brokers.Authentications
{
    public interface IAuthenticationBroker
    {
        ValueTask ChallengeExternalLoginAsync(string provider, string redirectUrl);
        string CreateToken(Dictionary<string, object> claimsDict);
        ClaimsPrincipal GetClaimsPrincipal();
        ValueTask<ExternalLoginInfo> GetExternalLoginInfoAsync();
        ValueTask SignInAsync(Dictionary<string, object> claimsDictionary, bool isPersistent, string authenticationMethod);
        ValueTask SignOutAsync();
    }
}
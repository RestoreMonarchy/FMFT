using FMFT.Extensions.Authentication.Models;
using System.Security.Claims;

namespace FMFT.Web.Server.Brokers.Authentications
{
    public interface IAuthenticationBroker
    {
        bool IsAuthenticated { get; }
        bool IsNotAuthenticated { get; }
        int AuthenticatedUserId { get; }

        ValueTask ChallengeExternalLoginAsync(string provider, string redirectUrl);
        ClaimsPrincipal GetClaimsPrincipal();
        ValueTask<ExternalLoginInfo> GetExternalLoginInfoAsync();
        ValueTask SignInAsync(Dictionary<string, object> claimsDictionary, bool isPersistent, string authenticationMethod);
        ValueTask SignOutAsync();
    }
}
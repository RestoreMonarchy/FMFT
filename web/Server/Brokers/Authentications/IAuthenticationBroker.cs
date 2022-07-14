using FMFT.Extensions.Authentication.Models;

namespace FMFT.Web.Server.Brokers.Authentications
{
    public interface IAuthenticationBroker
    {
        bool IsAuthenticated { get; }
        bool IsNotAuthenticated { get; }
        int AuthenticatedUserId { get; }

        ValueTask ChallengeExternalLoginAsync(string provider, string redirectUrl);
        ValueTask<ExternalLoginInfo> GetExternalLoginInfoAsync();
        ValueTask SignInAsync(Dictionary<string, object> claimsDictionary, bool isPersistent, string authenticationMethod);
        ValueTask SignOutAsync();
    }
}
using FMFT.Extensions.Authentication.Client;
using System.Security.Claims;

namespace FMFT.Web.Client.Brokers.Authentications
{
    public class AuthenticationBroker : IAuthenticationBroker
    {
        private readonly AuthenticationManager manager;

        public AuthenticationBroker(AuthenticationManager manager)
        {
            this.manager = manager;
        }

        public void SignIn(IDictionary<string, object> claimsDictionary)
        {
            IEnumerable<Claim> claims = DictionaryToClaims(claimsDictionary);
            manager.SignIn(claims);
        }

        public void SignOut()
        {
            manager.SignOut();
        }

        private IEnumerable<Claim> DictionaryToClaims(IDictionary<string, object> claimsDictionary)
        {
            return claimsDictionary.Select(x => new Claim(x.Key, x.Value?.ToString() ?? string.Empty));
        }
    }
}

using FMFT.Extensions.Authentication.Server;
using System.Security.Claims;

namespace FMFT.Web.Server.Brokers.Authentications
{
    public class AuthenticationBroker : IAuthenticationBroker
    {
        private readonly AuthenticationContext context;

        public AuthenticationBroker(IHttpContextAccessor httpContextAccessor)
        {
            context = new AuthenticationContext(httpContextAccessor.HttpContext);
        }

        public bool IsAuthenticated => context.IsAuthenticated;
        public bool IsNotAuthenticated => !IsAuthenticated;

        public int AuthenticatedUserId
        {
            get
            {
                string userIdString = context.FindClaimValue(ClaimTypes.NameIdentifier);
                return Convert.ToInt32(userIdString);
            }
        }

        public async ValueTask SignOutAsync()
        {
            await context.SignOutAsync();
        }

        public async ValueTask SignInAsync(
            Dictionary<string, object> claimsDictionary, 
            bool isPersistent, 
            string authenticationMethod)
        {
            List<Claim> claims = DictionaryToClaims(claimsDictionary);

            await context.SignInAsync(claims, isPersistent, authenticationMethod);
        }

        private List<Claim> DictionaryToClaims(Dictionary<string, object> claims)
        {
            return claims.Select(x => new Claim(x.Key, x.Value.ToString())).ToList();
        }
    }
}

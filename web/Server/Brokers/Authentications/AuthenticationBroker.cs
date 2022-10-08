using FMFT.Extensions.Authentication;
using FMFT.Extensions.Authentication.Models;
using FMFT.Web.Server.Models.Options.Authentications;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace FMFT.Web.Server.Brokers.Authentications
{
    public class AuthenticationBroker : IAuthenticationBroker
    {
        private readonly HttpContext httpContext;
        private readonly JWTAuthenticationOptions options;
        private readonly AuthenticationContext context;

        public AuthenticationBroker(IHttpContextAccessor httpContextAccessor, IOptions<JWTAuthenticationOptions> options)
        {
            this.httpContext = httpContextAccessor.HttpContext;
            this.options = options.Value;
            this.context = GetAuthenticationContext();            
        }

        private AuthenticationContext GetAuthenticationContext()
        {
            JWTOptions jwtOptions = new()
            {
                Issuer = options.Issuer,
                Audience = options.Audience,
                Key = options.SymmetricSecurityKey,
                Algorithm = options.Algorithm
            };

            return new AuthenticationContext(httpContext, jwtOptions);
        }

        public ClaimsPrincipal GetClaimsPrincipal()
        {
            return context.ClaimsPrincipal;
        }

        public string CreateToken(Dictionary<string, object> claimsDict)
        {
            List<Claim> claims = DictionaryToClaims(claimsDict);
            return context.CreateToken(claims, DateTime.Now.AddMinutes(15));            
        }

        private List<Claim> DictionaryToClaims(Dictionary<string, object> claimsDict)
        {
            return claimsDict.Select(x => new Claim(x.Key, x.Value.ToString())).ToList();
        }
    }
}

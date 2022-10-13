using FMFT.Extensions.Authentication;
using FMFT.Extensions.Authentication.Models;
using FMFT.Web.Server.Models.Options.Authentications;
using Microsoft.Extensions.Options;

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
                Key = options.KeyBytes
            };

            return new AuthenticationContext(httpContext, jwtOptions);
        }

        public T GetTokenPayload<T>()
        {
            return context.GetTokenPayload<T>();
        }

        public string CreateToken<T>(T payload)
        {
            return context.CreateToken(payload, TimeSpan.FromDays(7));            
        }
    }
}

using FMFT.Extensions.Authentication.Models;
using FMFT.Extensions.Authentication.Models.Exceptions;
using LitJWT;
using LitJWT.Algorithms;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Security.Cryptography;

namespace FMFT.Extensions.Authentication
{
    public class AuthenticationContext
    {
        private readonly HttpContext httpContext;
        private readonly JWTOptions options;

        public AuthenticationContext(HttpContext httpContext, JWTOptions options)
        {
            this.httpContext = httpContext;
            this.options = options;
        }

        public string CreateToken<T>(T payload, TimeSpan expireTime)
        {
            byte[] key = options.Key;

            HS256Algorithm algorithm = new(key);
            JwtEncoder encoder = new(algorithm);

            string token = encoder.Encode(payload, expireTime);
            return token;
        }

        public bool IsAuthenticated => httpContext.User?.Identity?.IsAuthenticated ?? false;
        public ClaimsPrincipal ClaimsPrincipal 
        { 
            get
            {
                if (!IsAuthenticated)
                    throw new NotAuthenticatedException();

                return httpContext.User;
            } 
        }   
    }
}
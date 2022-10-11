using FMFT.Extensions.Authentication.Models;
using FMFT.Extensions.Authentication.Models.Exceptions;
using LitJWT;
using LitJWT.Algorithms;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;

namespace FMFT.Extensions.Authentication
{
    public class AuthenticationContext
    {
        private readonly HttpContext httpContext;
        private readonly JWTOptions options;

        private readonly IJwtAlgorithm algorithm;

        public AuthenticationContext(HttpContext httpContext, JWTOptions options)
        {
            this.httpContext = httpContext;
            this.options = options;

            algorithm = new HS256Algorithm(options.Key);
        }

        public string CreateToken<T>(T payload, TimeSpan expireTime)
        {
            
            JwtEncoder encoder = new(algorithm);

            string token = encoder.Encode(payload, expireTime);
            return token;
        }

        public string RetrieveToken()
        {
            if (httpContext.Request.Headers.TryGetValue(HeaderNames.Authorization, out StringValues token))
            {
                return token.ToString();
            }

            throw new MissingAuthorizationHeaderException();
        }

        public T GetTokenPayload<T>()
        {
            string token = RetrieveToken();

            JwtDecoder decoder = new(algorithm);

            // Decode and verify, you can check the result.
            T payloadObj;
            DecodeResult result = decoder.TryDecode(token, out payloadObj);

            if (result == DecodeResult.Success)
            {
                return payloadObj;
            }

            throw new InvalidAuthenticationTokenException($"Invalid jwt token. Result: {result}");
        } 
    }
}
using FMFT.Extensions.Authentication.Models;
using FMFT.Extensions.Authentication.Constants;
using FMFT.Extensions.Authentication.Extensions;
using FMFT.Extensions.Authentication.Models.Exceptions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Security.Principal;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace FMFT.Extensions.Authentication
{
    public class AuthenticationContext
    {
        private readonly HttpContext httpContext;
        private readonly JWTOptions options;
        private readonly SigningCredentials signingCredentials;

        public AuthenticationContext(HttpContext httpContext, JWTOptions options)
        {
            this.httpContext = httpContext;
            this.options = options;

            signingCredentials = new(options.Key, options.Algorithm);
        }

        public string CreateToken(IList<Claim> claims, DateTime expireDate)
        {
            JwtSecurityTokenHandler tokenHandler = new();

            ClaimsIdentity identity = new(claims);

            SecurityToken token = tokenHandler.CreateJwtSecurityToken(new SecurityTokenDescriptor
            {
                Audience = options.Audience,
                Issuer = options.Issuer,
                SigningCredentials = signingCredentials,
                Expires = expireDate.ToUniversalTime(),
                Subject = identity
            });

            return tokenHandler.WriteToken(token);
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

        public string FindClaimValue(string claimType)
        {
            if (!IsAuthenticated)
                throw new NotAuthenticatedException();

            return httpContext.User.FindFirstValue(claimType);
        }            

        public async ValueTask SignInAsync(IList<Claim> claims, bool isPersistent, string authenticationMethod)
        {
            if (authenticationMethod != null)
            {
                await httpContext.SignOutAsync(FMFTAuthenticationDefaults.ExternalScheme);
            }

            AuthenticationProperties properties = new()
            {
                IsPersistent = isPersistent
            };

            if (authenticationMethod != null)
            {
                Claim authenticationMethodClaim = new(ClaimTypes.AuthenticationMethod, authenticationMethod);
                claims.Add(authenticationMethodClaim);
            }

            ClaimsIdentity claimsIdentity = new(FMFTAuthenticationDefaults.ApplicationScheme);
            claimsIdentity.AddClaims(claims);
            ClaimsPrincipal claimsPrincipal = new(claimsIdentity);
            await httpContext.SignInAsync(FMFTAuthenticationDefaults.ApplicationScheme, claimsPrincipal, properties);
        }

        public async ValueTask SignOutAsync()
        {
            await httpContext.SignOutAsync(FMFTAuthenticationDefaults.ApplicationScheme);
            await httpContext.SignOutAsync(FMFTAuthenticationDefaults.ExternalScheme);
            httpContext.User = null;
        }

        public async ValueTask ChallengeExternalLoginAsync(string provider, string redirectUrl)
        {
            AuthenticationProperties authenticationProperties = 
                ConfigureExternalAuthenticationProperties(provider, redirectUrl);

            await httpContext.ChallengeAsync(provider, authenticationProperties);
        }

        public AuthenticationProperties ConfigureExternalAuthenticationProperties(
            string provider, 
            string redirectUrl)
        {
            AuthenticationProperties properties = new() 
            { 
                RedirectUri = redirectUrl 
            };
            properties.Items[FMFTAuthenticationDefaults.LoginProviderKey] = provider;

            return properties;
        }

        public async ValueTask<ExternalLoginInfo> GetExternalLoginInfoAsync()
        {
            AuthenticateResult auth = await httpContext.AuthenticateAsync(
                FMFTAuthenticationDefaults.ExternalScheme);

            IDictionary<string, string> items = auth?.Properties?.Items;
            if (auth?.Principal == null 
                || items == null 
                || !items.ContainsKey(FMFTAuthenticationDefaults.LoginProviderKey))
            {
                throw new ExternalNotAuthenticatedException();
            }

            string providerKey = auth.Principal.FindFirstValue(ClaimTypes.NameIdentifier);
            string provider = items[FMFTAuthenticationDefaults.LoginProviderKey];

            ExternalLoginInfo info = new()
            {
                Principal = auth.Principal,
                ProviderName = provider,
                ProviderKey = providerKey
            };

            return info;
        }
    }
}
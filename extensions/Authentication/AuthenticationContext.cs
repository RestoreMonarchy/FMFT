using FMFT.Extensions.Authentication.Constants;
using FMFT.Extensions.Authentication.Extensions;
using FMFT.Extensions.Authentication.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace FMFT.Extensions.Authentication
{
    public class AuthenticationContext
    {
        private readonly HttpContext httpContext;

        public AuthenticationContext(HttpContext httpContext)
        {
            this.httpContext = httpContext;
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
            await httpContext.SignInAsync(claimsPrincipal, properties);
        }

        public async ValueTask SignOutAsync()
        {
            await httpContext.SignOutAsync(FMFTAuthenticationDefaults.ApplicationScheme);
            await httpContext.SignOutAsync(FMFTAuthenticationDefaults.ExternalScheme);
            httpContext.User = null;
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
                return null;
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
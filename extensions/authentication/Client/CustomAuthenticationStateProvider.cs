using FMFT.Extensions.Authentication.Shared.Constants;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace FMFT.Extensions.Authentication.Client
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private Task<AuthenticationState> AuthenticationState { get; set; }

        public void SetAuthenticationState(IEnumerable<Claim> claims)
        {
            ClaimsIdentity claimsIdentity = new(FMFTAuthenticationDefaults.ApplicationScheme);
            claimsIdentity.AddClaims(claims);
            ClaimsPrincipal principal = new(claimsIdentity);
            AuthenticationState authenticationState = new(principal);
            AuthenticationState = Task.FromResult(authenticationState);

            NotifyAuthenticationStateChanged(AuthenticationState);
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return AuthenticationState;
        }
    }
}
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace FMFT.Extensions.Authentication.Client
{
    public class AuthenticationManager
    {
        private readonly CustomAuthenticationStateProvider stateProvider;

        public AuthenticationManager(AuthenticationStateProvider stateProvider)
        {
            if (stateProvider is not CustomAuthenticationStateProvider)
            {
                throw new NotSupportedException(
                    $"To use {nameof(AuthenticationManager)} {nameof(CustomAuthenticationStateProvider)} is required");
            }   
            
            this.stateProvider = stateProvider as CustomAuthenticationStateProvider;
        }

        public void SignIn(IEnumerable<Claim> claims)
        {
            stateProvider.SetAuthenticationState(claims);
        }

        public void SignOut()
        {
            stateProvider.SetAuthenticationState(null);
        }
    }
}

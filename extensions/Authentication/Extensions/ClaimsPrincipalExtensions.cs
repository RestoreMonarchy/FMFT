using System.Security.Claims;

namespace FMFT.Extensions.Authentication.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string FindFirstValue(this ClaimsPrincipal principal, string claimType)
        {
            return principal.FindFirst(claimType)?.Value ?? null;
        }
    }
}

using System.Security.Claims;

namespace FMFT.Extensions.Authentication.Models
{
    public class ExternalLoginInfo
    {
        public ClaimsPrincipal Principal { get; set; }
        public string ProviderName { get; set; }
        public string ProviderKey { get; set; }
    }
}

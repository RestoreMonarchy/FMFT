namespace FMFT.Extensions.Authentication.Constants
{
    public class FMFTAuthenticationDefaults
    {
        private const string CookiePrefix = "FMFT.Authentication";

        public static readonly string ApplicationScheme = CookiePrefix + ".Application";
        public static readonly string ExternalScheme = CookiePrefix + ".External";

        public const string LoginProviderKey = "LoginProvider";
    }
}

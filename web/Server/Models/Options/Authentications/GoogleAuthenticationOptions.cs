namespace FMFT.Web.Server.Models.Options.Authentications
{
    public class GoogleAuthenticationOptions
    {
        public const string SectionKey = "Authentication:Google";

        public static GoogleAuthenticationOptions FromConfiguration(IConfiguration configuration)
        {
            return configuration.GetSection(SectionKey).Get<GoogleAuthenticationOptions>();
        }

        public bool Enabled { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}

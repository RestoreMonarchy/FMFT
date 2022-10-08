namespace FMFT.Web.Server.Models.Options.Authentications
{
    public class FacebookAuthenticationOptions
    {
        public const string SectionKey = "Authentication:Facebook";

        public static FacebookAuthenticationOptions FromConfiguration(IConfiguration configuration)
        {
            return configuration.GetSection(SectionKey).Get<FacebookAuthenticationOptions>();
        }

        public bool Enabled { get; set; }
        public string AppId { get; set; }
        public string AppSecret { get; set; }
    }
}

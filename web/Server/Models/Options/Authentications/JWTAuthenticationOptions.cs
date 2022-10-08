using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;

namespace FMFT.Web.Server.Models.Options.Authentications
{
    public class JWTAuthenticationOptions
    {
        public const string SectionKey = "Authentication:Jwt";

        public static JWTAuthenticationOptions FromConfiguration(IConfiguration configuration)
        {
            return configuration.GetSection(SectionKey).Get<JWTAuthenticationOptions>();
        }

        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }

        [JsonIgnore]
        public byte[] KeyBytes => Encoding.UTF8.GetBytes(Key);
        [JsonIgnore]
        public SymmetricSecurityKey SymmetricSecurityKey => new SymmetricSecurityKey(KeyBytes);
    }
}

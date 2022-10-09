namespace FMFT.Extensions.Authentication.Models
{
    public class JWTOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public byte[] Key { get; set; }
    }
}

namespace FMFT.Extensions.Blazor.Facebook.Models.Results
{
    public partial class FacebookLoginResult
    {
        public class Response
        {
            public string AccessToken { get; set; }
            public string UserID { get; set; }
            public int ExpiresIn { get; set; }
            public string SignedRequest { get; set; }
            public string GraphDomain { get; set; }
            public int Data_access_expiration_time { get; set; }
        }
    }
}

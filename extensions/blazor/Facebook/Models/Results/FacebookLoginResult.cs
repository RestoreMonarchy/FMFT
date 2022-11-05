using System.Text.Json.Serialization;

namespace FMFT.Extensions.Blazor.Facebook.Models.Results
{
    public partial class FacebookLoginResult
    {
        public Response AuthResponse { get; set; }
        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public FacebookLoginStatus Status { get; set; }
    }
}

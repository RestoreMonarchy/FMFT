using System.ComponentModel;

namespace FMFT.Extensions.Blazor.Facebook.Models.Results
{
    public enum FacebookLoginStatus
    {
        [Description("unkown")]
        Unkown,
        [Description("connected")]
        Connected,
        [Description("not_authorized")]
        NotAuthorized        
    }
}

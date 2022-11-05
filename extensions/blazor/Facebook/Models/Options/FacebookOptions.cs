using FMFT.Extensions.Blazor.Facebook.Models.Results;

namespace FMFT.Extensions.Blazor.Facebook.Models.Options
{
    public class FacebookOptions
    {
        public string AppId { get; set; }

        public Func<IServiceProvider, FacebookLoginResult, Task> OnLogin { get; set; } 
            = (services, result) => Task.CompletedTask;
    }
}

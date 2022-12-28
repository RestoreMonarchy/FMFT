using FMFT.Extensions.Blazor.Google.Models.Results;

namespace FMFT.Extensions.Blazor.Google.Models.Options
{
    public class GoogleOptions
    {
        public string ClientId { get; set; }
        public Func<IServiceProvider, GoogleLoginResult, Task> OnLogin { get; set; }
            = (services, result) => Task.CompletedTask;
    }
}

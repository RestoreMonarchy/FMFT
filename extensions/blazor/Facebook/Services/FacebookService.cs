using FMFT.Extensions.Blazor.Facebook.Models.Options;
using FMFT.Extensions.Blazor.Facebook.Models.Results;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace FMFT.Extensions.Blazor.Facebook.Services
{
    public class FacebookService
    {
        private readonly FacebookOptions options;
        private readonly IJSRuntime jsRuntime;
        private readonly IServiceProvider serviceProvider;

        private readonly DotNetObjectReference<FacebookService> objectReference;

        public FacebookService(IOptions<FacebookOptions> options, IJSRuntime jsRuntime, IServiceProvider serviceProvider)
        {
            this.options = options.Value;
            this.jsRuntime = jsRuntime;
            this.serviceProvider = serviceProvider;

            objectReference = DotNetObjectReference.Create(this);
        }

        private bool isInitialized = false;

        public async ValueTask InitializeAsync()
        {
            if (isInitialized)
                return;

            await jsRuntime.InvokeVoidAsync("fbAsyncInit", options.AppId);
            isInitialized = true;
        }

        public async ValueTask LoginAsync()
        {
            await jsRuntime.InvokeVoidAsync("fbLogin", objectReference);            
        }

        [JSInvokable]
        public async Task HandleFacebookLoginCallbackAsync(FacebookLoginResult result)
        {
            await options.OnLogin(serviceProvider, result);

            Console.WriteLine("===== FACEBOOK RESULT =====");
            Console.WriteLine(result);
            Console.WriteLine($"{JsonSerializer.Serialize(result)}");            
            Console.WriteLine("===========================");
        }
    }
}
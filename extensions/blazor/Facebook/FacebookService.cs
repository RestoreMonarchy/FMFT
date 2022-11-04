using FMFT.Extensions.Blazor.Facebook.Models;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;

namespace FMFT.Extensions.Blazor.Facebook
{
    public class FacebookService
    {
        private readonly FacebookOptions options;
        private readonly IJSRuntime jsRuntime;

        private readonly DotNetObjectReference<FacebookService> objectReference;

        public FacebookService(IOptions<FacebookOptions> options, IJSRuntime jsRuntime)
        {
            this.options = options.Value;
            this.jsRuntime = jsRuntime;

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
        public async Task HandleFacebookLoginCallbackAsync(object result)
        {
            if (options.LoginCallback.HasValue)
            {
                await options.LoginCallback.Value;
            } else
            {
                Console.WriteLine("===== FACEBOOK RESULT =====");
                Console.WriteLine(result);
                Console.WriteLine("===========================");
            }
        }
    }
}
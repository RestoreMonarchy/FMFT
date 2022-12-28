using FMFT.Extensions.Blazor.Google.Models.Options;
using FMFT.Extensions.Blazor.Google.Models.Results;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;
using System.Text.Json;

namespace FMFT.Extensions.Blazor.Google.Services
{
    public class GoogleService
    {
        private readonly GoogleOptions options;
        private readonly IJSRuntime jsRuntime;
        private readonly IServiceProvider serviceProvider;

        private readonly DotNetObjectReference<GoogleService> objectReference;

        public GoogleService(IOptions<GoogleOptions> options, IJSRuntime jsRuntime, IServiceProvider serviceProvider)
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

            await jsRuntime.InvokeVoidAsync("googleAsyncInit", options.ClientId, objectReference);
            isInitialized = true;
        }

        public async ValueTask LoginAsync()
        {
            await jsRuntime.InvokeVoidAsync("googleLogin");
        }

        [JSInvokable]
        public async Task HandleGoogleLoginCallbackAsync(GoogleLoginResult result)
        {
            await options.OnLogin(serviceProvider, result);

            Console.WriteLine("===== GOOGLE RESULT =====");
            Console.WriteLine(result);
            Console.WriteLine($"{JsonSerializer.Serialize(result)}");
            Console.WriteLine("===========================");
        }
    }
}

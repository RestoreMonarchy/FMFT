using Microsoft.JSInterop;

namespace FMFT.Web.Client.Brokers.JSRuntimes
{
    public class JSRuntimeBroker : IJSRuntimeBroker
    {
        private readonly IJSRuntime jsRuntime;

        public JSRuntimeBroker(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        public async ValueTask BuildSeatsCanvas<T>(string canvasId, DotNetObjectReference<T> objectReference) where T : class
        {
            await jsRuntime.InvokeVoidAsync("BuildSeatsCanvas", canvasId, objectReference);
        }
    }
}

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

        public async ValueTask<object> InitializeSeatsCanvasAsync<T>(object options, DotNetObjectReference<T> objectReference) where T : class
        {
            return await jsRuntime.InvokeAsync<object>("InitializeSeatsCanvas", options, objectReference);
        }

        public async ValueTask DrawSeatAsync(object options, int row, int column, string color)
        {
            await jsRuntime.InvokeVoidAsync("DrawSeat", options, row, column, color);
        }
    }
}

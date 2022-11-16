using Microsoft.AspNetCore.Components;
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

        public async ValueTask ClearModalBackdropAsync()
        {
            await jsRuntime.InvokeVoidAsync("ClearModalBackdrop");
        }

        public async ValueTask HideNavbarCollapseAsync(ElementReference navbarContent)
        {
            await jsRuntime.InvokeVoidAsync("HideNavbarCollapse", navbarContent);
        }

        public async ValueTask InitializeFacebookAsync()
        {
            await jsRuntime.InvokeVoidAsync("fbAsyncInit");
        }

        public async ValueTask ProcessFacebookLoginAsync()
        {
            await jsRuntime.InvokeVoidAsync("fbLogin");
        }

        public async ValueTask StartCarouselAsync(ElementReference carousel)
        {
            await jsRuntime.InvokeVoidAsync("StartCarousel", carousel);
        }
    }
}

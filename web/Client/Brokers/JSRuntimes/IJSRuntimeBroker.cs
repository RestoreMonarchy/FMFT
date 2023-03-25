using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace FMFT.Web.Client.Brokers.JSRuntimes
{
    public interface IJSRuntimeBroker
    {
        ValueTask ClearModalBackdropAsync();
        ValueTask DownloadFromByteArrayAsync(byte[] byteArray, string fileName, string contentType);
        ValueTask DrawSeatAsync(object options, int row, int column, int sector, string color);
        ValueTask HideNavbarCollapseAsync(ElementReference navbarContent);
        ValueTask InitializeFacebookAsync();
        ValueTask<object> InitializeSeatsCanvasAsync<T>(object options, DotNetObjectReference<T> objectReference) where T : class;
        ValueTask ProcessFacebookLoginAsync();
    }
}

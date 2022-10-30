using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace FMFT.Web.Client.Brokers.JSRuntimes
{
    public interface IJSRuntimeBroker
    {
        ValueTask ClearModalBackdropAsync();
        ValueTask DrawSeatAsync(object options, int row, int column, string color);
        ValueTask<object> InitializeSeatsCanvasAsync<T>(object options, DotNetObjectReference<T> objectReference) where T : class;
    }
}

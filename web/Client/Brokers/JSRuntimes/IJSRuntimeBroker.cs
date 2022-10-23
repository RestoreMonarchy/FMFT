using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace FMFT.Web.Client.Brokers.JSRuntimes
{
    public interface IJSRuntimeBroker
    {
        ValueTask BuildSeatsCanvas<T>(string canvasId, int[] seatsMap, object options, DotNetObjectReference<T> objectReference) where T : class;
    }
}

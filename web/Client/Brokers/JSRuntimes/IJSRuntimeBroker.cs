using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace FMFT.Web.Client.Brokers.JSRuntimes
{
    public interface IJSRuntimeBroker
    {
        ValueTask BuildSeatsCanvas<T>(string canvasId, DotNetObjectReference<T> objectReference) where T : class;
    }
}

using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Brokers.JSRuntimes
{
    public interface IJSRuntimeBroker
    {
        ValueTask InitializePanzoomElementAsync(ElementReference containerElement);
    }
}

using Microsoft.AspNetCore.Components;

namespace FMFT.Client.Brokers.JSRuntimes
{
    public interface IJSRuntimeBroker
    {
        ValueTask InitializePanzoomElementAsync(ElementReference containerElement);
    }
}

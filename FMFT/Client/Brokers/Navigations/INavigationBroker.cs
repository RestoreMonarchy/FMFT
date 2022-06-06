using Microsoft.AspNetCore.Components.Routing;

namespace FMFT.Client.Brokers.Navigations
{
    public interface INavigationBroker
    {
        string Uri { get; }
        string AbsoluteUri { get; }
        string RelativeUri { get; }

        event Action<LocationChangedEventArgs> OnLocationChange;
    }
}

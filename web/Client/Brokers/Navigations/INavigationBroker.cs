using Microsoft.AspNetCore.Components.Routing;

namespace FMFT.Web.Client.Brokers.Navigations
{
    public interface INavigationBroker
    {
        string Uri { get; }
        string AbsoluteUri { get; }
        string RelativeUri { get; }

        event Action<LocationChangedEventArgs> OnLocationChange;

        void ForceLoadNavigateTo(string url);
        void NavigateTo(string url);
    }
}

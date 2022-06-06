using FMFT.Client.Brokers.Navigations;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace FMFT.Client.Shared.Navbars
{
    public partial class MainNavbar
    {
        [Inject]
        public INavigationBroker NavigationBroker { get; set; }

        protected override void OnInitialized()
        {
            NavigationBroker.OnLocationChange += HandleLocationChanged;
        }

        public void HandleLocationChanged(LocationChangedEventArgs args)
        {
            StateHasChanged();
        }
    }
}

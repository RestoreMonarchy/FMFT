using FMFT.Web.Client.Brokers.Navigations;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace FMFT.Web.Client.Shared.Navbars
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

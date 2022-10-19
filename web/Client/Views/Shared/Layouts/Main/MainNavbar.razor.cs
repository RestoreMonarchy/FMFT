using FMFT.Web.Client.Brokers.Navigations;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace FMFT.Web.Client.Views.Shared.Layouts.Main
{
    public partial class MainNavbar
    {
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

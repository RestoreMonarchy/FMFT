using FMFT.Web.Client.Services.Accounts;
using FMFT.Web.Client.Views.Shared.Components.Dialogs;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace FMFT.Web.Client.Views.Shared.Layouts.Main
{
    public partial class MainNavbar
    {
        [Parameter]
        public string Class { get; set; }

        public LogoutDialog LogoutDialog { get; set; }

        public ElementReference NavbarContent { get; set; }

        protected override void OnInitialized()
        {
            NavigationBroker.OnLocationChange += HandleLocationChanged;
        }

        public async void HandleLocationChanged(LocationChangedEventArgs args)
        {
            await JSRuntimeBroker.HideNavbarCollapseAsync(NavbarContent);
            StateHasChanged();
        }

        private async Task HandleLogoutAsync()
        {
            await LogoutDialog.ShowAsync();
        }
    }
}

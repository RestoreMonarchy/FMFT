using FMFT.Web.Client.Services.Accounts;
using FMFT.Web.Client.Views.Shared.Components.Dialogs;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace FMFT.Web.Client.Views.Shared.Layouts.Main
{
    public partial class MainNavbar
    {
        public LogoutDialog LogoutDialog { get; set; }

        protected override void OnInitialized()
        {
            NavigationBroker.OnLocationChange += HandleLocationChanged;
        }

        public void HandleLocationChanged(LocationChangedEventArgs args)
        {
            StateHasChanged();
        }

        private async Task HandleLogoutAsync()
        {
            await LogoutDialog.ShowAsync();
        }
    }
}

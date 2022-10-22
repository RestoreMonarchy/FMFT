using FMFT.Web.Client.Services.Accounts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace FMFT.Web.Client.Views.Shared.Layouts.Main
{
    public partial class MainNavbar
    {
        [Inject]
        public IAccountService AccountService { get; set; }

        protected override void OnInitialized()
        {
            NavigationBroker.OnLocationChange += HandleLocationChanged;
        }

        public void HandleLocationChanged(LocationChangedEventArgs args)
        {
            StateHasChanged();
        }

        private async Task LogoutAsync()
        {
            await AccountService.LogoutAsync();
            NavigationBroker.NavigateTo("/");
        }
    }
}

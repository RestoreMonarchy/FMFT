using FMFT.Extensions.Blazor.Bases.Buttons;
using FMFT.Extensions.Blazor.Bases.Dialogs;
using FMFT.Web.Client.Services.Accounts;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Shared.Components.Dialogs
{
    public partial class LogoutDialog
    {
        [Inject]
        public IAccountService AccountService { get; set; }

        public ModalDialog LogoutModalDialog { get; set; }
        public ButtonBase LogoutButton { get; set; }

        public async Task ShowAsync()
        {
            await LogoutModalDialog.ShowAsync();
        }

        private async Task HandleLogoutAsync()
        {
            LogoutButton.StartSpinning();
            await LogoutAsync(); 
            LogoutButton.StopSpinning();
            await LogoutModalDialog.HideAsync();

        }

        private async Task LogoutAsync()
        {
            await AccountService.LogoutAsync();
            NavigationBroker.NavigateTo("/");
        }
    }
}

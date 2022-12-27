using FMFT.Extensions.Blazor.Bases.Dialogs;
using FMFT.Web.Client.Views.Shared.Components.Dialogs;

namespace FMFT.Web.Client.Views.Shared.Layouts.Main
{
    public partial class MainNavbarAlert
    {
        public SendConfirmEmailDialog SendConfirmEmailDialog { get; set; }


        protected override void OnInitialized()
        {
            UserAccountState.OnChange += StateHasChanged;
        }

        private async Task HandleShowDialogAsync()
        {
            await SendConfirmEmailDialog.ShowAsync();
        }
    }
}

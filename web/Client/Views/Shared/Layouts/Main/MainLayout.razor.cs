using FMFT.Web.Client.Brokers.Storages;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace FMFT.Web.Client.Views.Shared.Layouts.Main
{
    public partial class MainLayout
    {

        public ErrorBoundary ErrorBoundary { get; set; }

        private bool displayCookies = false;

        protected override void OnParametersSet()
        {
            ErrorBoundary?.Recover();    
        }

        protected override async Task OnInitializedAsync()
        {
            bool cookiesAlertFlag = await StorageBroker.GetCookiesAlertFlagAsync();
            displayCookies = !cookiesAlertFlag;
        }

        protected override async Task OnParametersSetAsync()
        {
            await JSRuntimeBroker.ClearModalBackdropAsync();
        }

        private async Task HideCookiesAlertAsync()
        {
            displayCookies = false;
            await StorageBroker.SetCookiesAlertFlagAsync(true);
        }
    }
}

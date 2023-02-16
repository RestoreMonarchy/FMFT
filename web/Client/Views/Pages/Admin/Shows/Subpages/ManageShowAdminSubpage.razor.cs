using FMFT.Extensions.Blazor.Bases.Alerts;
using FMFT.Extensions.Blazor.Bases.Buttons;
using FMFT.Extensions.Blazor.Bases.Loadings;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Shows;
using FMFT.Web.Client.Models.API.Shows.Requests;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Pages.Admin.Shows.Subpages
{
    public partial class ManageShowAdminSubpage
    {
        [Parameter]
        public Show Show { get; set; }
        [Parameter]
        public EventCallback<Show> ShowChanged { get; set; }

        public AlertGroupBase AlertGroup { get; set; }
        public AlertBase SuccessAlert { get; set; }
        public AlertBase ErrorAlert { get; set; }
        public ButtonBase UpdateStatusButton { get; set; }

        private async Task UpdateStatusAsync(bool isEnabled)
        {
            AlertGroup.HideAll();
            UpdateStatusButton.StartSpinning();

            UpdateShowStatusRequest request = new()
            {
                ShowId = Show.Id,
                IsEnabled = isEnabled
            };

            APIResponse<Show> response = await APIBroker.UpdateShowStatusAsync(request);

            if (response.IsSuccessful)
            {
                Show = response.Object;
                await ShowChanged.InvokeAsync(Show);
                SuccessAlert.Show();
            } else
            {
                ErrorAlert.Show();
            }

            UpdateStatusButton.StopSpinning();
        }
    }
}

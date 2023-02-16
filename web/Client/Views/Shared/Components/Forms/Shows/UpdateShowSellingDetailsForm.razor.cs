using FMFT.Extensions.Blazor.Bases.Alerts;
using FMFT.Extensions.Blazor.Bases.Buttons;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Shows;
using FMFT.Web.Client.Models.API.Shows.Requests;
using FMFT.Web.Client.Models.Forms.Shows;
using FMFT.Web.Shared.Extensions;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Shared.Components.Forms.Shows
{
    public partial class UpdateShowSellingDetailsForm
    {
        [Parameter]
        public Show Show { get; set; }
        [Parameter]
        public EventCallback<Show> ShowChanged { get; set; }

        public AlertGroupBase AlertGroup { get; set; }
        public AlertBase SuccessAlert { get; set; }
        public AlertBase ErrorAlert { get; set; }
        public SubmitButtonBase SubmitButton { get; set; }

        private string calendarEndDate;
        public UpdateShowSellingDetailsFormModel Model { get; set; }

        public APIResponse<Show> Response { get; set; }

        protected override void OnParametersSet()
        {
            calendarEndDate = Show.EndDateTime.TruncateToMinuteStart().ToString("yyyy-MM-dd");

            Model = new()
            {
                SellStartDate = DateOnly.FromDateTime(Show.SellStartDateTime.LocalDateTime),
                SellStartTime = TimeOnly.FromDateTime(Show.SellStartDateTime.LocalDateTime)
            };
        }

        private async Task SubmitAsync()
        {
            AlertGroup.HideAll();
            SubmitButton.StartSpinning();

            DateTime sellStartDateTime = Model.SellStartDate.ToDateTime(Model.SellStartTime);

            UpdateShowSellingDetailsRequest request = new()
            {
                ShowId = Show.Id,
                SellStartDateTime = sellStartDateTime
            };

            Response = await APIBroker.UpdateShowSellingDetailsAsync(request);

            if (Response.IsSuccessful)
            {
                Show = Response.Object;
                await ShowChanged.InvokeAsync(Show);

                SuccessAlert.Show();
            }
            else
            {
                ErrorAlert.Show();
            }

            SubmitButton.StopSpinning();
        }
    }
}

using FMFT.Extensions.Blazor.Bases.Alerts;
using FMFT.Extensions.Blazor.Bases.Buttons;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Shows;
using FMFT.Web.Client.Models.API.Shows.Requests;
using FMFT.Web.Client.Models.Forms.Shows;
using FMFT.Web.Shared.Extensions;
using Microsoft.AspNetCore.Components;
using static FMFT.Extensions.Blazor.Facebook.Models.Results.FacebookLoginResult;

namespace FMFT.Web.Client.Views.Shared.Components.Forms.Shows
{
    public partial class UpdateShowTimeForm
    {
        [Parameter]
        public Show Show { get; set; }
        [Parameter]
        public EventCallback<Show> ShowChanged { get; set; }

        public UpdateShowTimeFormModel Model { get; set; }

        public AlertGroupBase AlertGroup { get; set; }
        public AlertBase SuccessAlert { get; set; }
        public AlertBase ValidationAlert { get; set; }
        public AlertBase ErrorAlert { get; set; }
        public SubmitButtonBase SubmitButton { get; set; }

        public APIResponse<Show> Response { get; set; }

        private string calendarStartDate = DateTime.Now.TruncateToMinuteStart().Date.ToString("yyyy-MM-dd");
        private string calendarEndDate = DateTime.Now.AddMonths(12).TruncateToMinuteStart().ToString("yyyy-MM-dd");

        protected override void OnParametersSet()
        {
            TimeSpan duration = Show.EndDateTime - Show.StartDateTime;

            Model = new()
            {
                StartDate = DateOnly.FromDateTime(Show.StartDateTime.LocalDateTime),
                StartTime = TimeOnly.FromDateTime(Show.StartDateTime.LocalDateTime),
                DurationMinutes = (int)duration.TotalMinutes
            };
        }

        private async Task SubmitAsync()
        {
            AlertGroup.HideAll();
            SubmitButton.StartSpinning();

            DateTime startDateTime = Model.StartDate.ToDateTime(Model.StartTime);
            DateTime endDateTime = startDateTime.AddMinutes(Model.DurationMinutes);

            UpdateShowTimeRequest request = new()
            {
                ShowId = Show.Id,
                StartDateTime = startDateTime,
                EndDateTime = endDateTime
            };

            Response = await APIBroker.UpdateShowTimeAsync(request);

            if (Response.IsSuccessful)
            {
                Show = Response.Object;
                await ShowChanged.InvokeAsync(Show);

                SuccessAlert.Show();
            }
            else
            {
                switch (Response.Error.Code)
                {
                    case "ERR013":
                        ValidationAlert.Show();
                        break;
                    default:
                        ErrorAlert.Show();
                        break;
                }
            }

            SubmitButton.StopSpinning();
        }
    }
}

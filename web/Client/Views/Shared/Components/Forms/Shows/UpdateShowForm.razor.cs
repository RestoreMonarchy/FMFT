using FMFT.Extensions.Blazor.Bases.Alerts;
using FMFT.Extensions.Blazor.Bases.Buttons;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Auditoriums;
using FMFT.Web.Client.Models.API.Shows;
using FMFT.Web.Client.Models.API.Shows.Requests;
using FMFT.Web.Client.Models.Forms.Shows;
using FMFT.Web.Shared.Extensions;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Shared.Components.Forms.Shows
{
    public partial class UpdateShowForm
    {
        [Parameter]
        public Show Show { get; set; }
        [Parameter]
        public EventCallback<Show> ShowChanged { get; set; }
        [Parameter]
        public List<Auditorium> Audutoriums { get; set; }

        public AlertGroupBase AlertGroup { get; set; }
        public AlertBase AuditoriumNotFoundAlert { get; set; }
        public AlertBase ShowNotFoundAlert { get; set; }
        public AlertBase ValidationAlert { get; set; }
        public AlertBase ErrorAlert { get; set; }
        public AlertBase SuccessAlert { get; set; }

        public SubmitButtonBase SubmitButton { get; set; }

        public APIResponse<Show> Response { get; set; }

        public UpdateShowFormModel Model { get; set; } = new();

        private string calendarStartDate = DateTime.Now.TruncateToMinuteStart().Date.ToString("yyyy-MM-dd");
        private string calendarEndDate = DateTime.Now.AddMonths(12).TruncateToMinuteStart().ToString("yyyy-MM-dd");

        private bool isPastStartDate => Show.StartDateTime.UtcDateTime < DateTime.UtcNow;
        private bool isPastEndDate => Show.EndDateTime.UtcDateTime < DateTime.UtcNow;

        private bool hasReservedSeats => Show.ReservedSeats.Any();

        protected override void OnParametersSet()
        {
            TimeSpan duration = Show.EndDateTime - Show.StartDateTime;

            Model = new()
            {
                Name = Show.Name,
                Description = Show.Description,
                StartDate = DateOnly.FromDateTime(Show.StartDateTime.LocalDateTime),
                StartTime = TimeOnly.FromDateTime(Show.StartDateTime.LocalDateTime),
                DurationMinutes = (int)duration.TotalMinutes,
                AudotiriumId = Show.AuditoriumId,
                ThumbnailMediaId = Show.ThumbnailMediaId
            };
        }

        private async Task SubmitAsync()
        {
            AlertGroup.HideAll();
            SubmitButton.StartSpinning();

            DateTime startDateTime = Model.StartDate.ToDateTime(Model.StartTime);
            DateTime endDateTime = startDateTime.AddMinutes(Model.DurationMinutes);

            UpdateShowRequest request = new()
            {
                Id = Show.Id,
                Name = Model.Name,
                Description = Model.Description,
                StartDateTime = new DateTimeOffset(startDateTime),
                EndDateTime = new DateTimeOffset(endDateTime),
                AuditoriumId = Model.AudotiriumId.Value,
                ThumbnailMediaId = Model.ThumbnailMediaId          
            };

            Response = await APIBroker.UpdateShowAsync(request);

            if (Response.IsSuccessful)
            {
                Show = Response.Object;
                await ShowChanged.InvokeAsync(Show);

                SuccessAlert.Show();
            } else
            {
                switch (Response.Error.Code)
                {
                    case "ERR014":
                        ShowNotFoundAlert.Show();
                        break;
                    case "ERR020":
                        AuditoriumNotFoundAlert.Show();
                        break;
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

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
    public partial class AddShowForm
    {
        [Parameter]
        public List<Auditorium> Auditoriums { get; set; }

        public AlertGroupBase AlertGroup { get; set; }
        public AlertBase AuditoriumNotFoundAlert { get; set; }
        public AlertBase ValidationAlert { get; set; }
        public AlertBase ErrorAlert { get; set; }
        public AlertBase SuccessAlert { get; set; }

        public SubmitButtonBase SubmitButton { get; set; }

        public APIResponse<Show> Response { get; set; }

        public Show Show => Response.Object;

        public AddShowFormModel Model { get; set; } = new()
        {
            StartDate = DateOnly.FromDateTime(DateTime.Now),
            StartTime = TimeOnly.MinValue,
            DurationMinutes = 120
        };

        private string calendarStartDate = DateTime.Now.TruncateToMinuteStart().Date.ToString("yyyy-MM-dd");
        private string calendarEndDate = DateTime.Now.AddMonths(12).TruncateToMinuteStart().ToString("yyyy-MM-dd");

        public async Task SubmitAsync()
        {
            AlertGroup.HideAll();
            SubmitButton.StartSpinning();

            DateTime startDateTime = Model.StartDate.ToDateTime(Model.StartTime);
            DateTime endDateTime = startDateTime.AddMinutes(Model.DurationMinutes);

            AddShowRequest request = new()
            {
                Name = Model.Name,
                Description = Model.Description,
                StartDateTime = startDateTime,
                EndDateTime = endDateTime,
                AuditoriumId = Model.AudotiriumId.Value,
                ThumbnailMediaId = Model.ThumbnailMediaId
            };

            Response = await APIBroker.AddShowAsync(request);

            if (Response.IsSuccessful)
            {
                SuccessAlert.Show();
                NavigationBroker.NavigateTo($"/admin/shows/{Show.Id}");
            }
            else
            {
                switch (Response.Error.Code)
                {
                    case "ERR020":
                        AuditoriumNotFoundAlert.Show();
                        break;
                    case "ERR012":
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

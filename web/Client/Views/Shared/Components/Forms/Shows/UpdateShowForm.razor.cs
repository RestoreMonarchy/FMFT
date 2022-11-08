using FMFT.Extensions.Blazor.Bases.Buttons;
using FMFT.Extensions.Blazor.Bases.Forms;
using FMFT.Extensions.Blazor.Bases.Inputs;
using FMFT.Web.Client.Models.API;
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

        public UpdateShowFormModel Model { get; set; } = new();


        private string calendarStartDate = DateTime.Now.TruncateToMinuteStart().Date.ToString("yyyy-MM-dd");
        private string calendarEndDate = DateTime.Now.AddMonths(12).TruncateToMinuteStart().ToString("s");

        private bool isPastStartDate => Show.StartDateTime.UtcDateTime < DateTime.UtcNow;
        private bool isPastEndDate => Show.EndDateTime.UtcDateTime < DateTime.UtcNow;

        protected override void OnParametersSet()
        {
            Model = new()
            {
                Name = Show.Name,
                Description = Show.Description,
                StartDate = DateOnly.FromDateTime(Show.StartDateTime.LocalDateTime),
                StartTime = TimeOnly.FromDateTime(Show.StartDateTime.LocalDateTime),
                EndDate = DateOnly.FromDateTime(Show.EndDateTime.LocalDateTime),
                EndTime = TimeOnly.FromDateTime(Show.EndDateTime.LocalDateTime),
                AudotiriumId = Show.AuditoriumId
            };
        }

        private async Task SubmitAsync()
        {
            DateTime startDateTime = Model.StartDate.ToDateTime(Model.StartTime);
            DateTime endDateTime = Model.EndDate.ToDateTime(Model.EndTime);

            UpdateShowRequest request = new()
            {
                Id = Show.Id,
                Name = Model.Name,
                Description = Model.Description,
                StartDateTime = new DateTimeOffset(startDateTime),
                EndDateTime = new DateTimeOffset(endDateTime),
                AuditoriumId = Model.AudotiriumId
            };

            Console.WriteLine(request.Id);

            APIResponse<Show> response = await APIBroker.UpdateShowAsync(request);
            Console.WriteLine($"response status code: {response.StatusCode}");
        }
    }
}

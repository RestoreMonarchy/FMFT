using FMFT.Web.Client.Models.Auditoriums.Exceptions;
using FMFT.Web.Client.Models.Auditoriums;
using FMFT.Web.Client.Models.Shows.Exceptions;
using FMFT.Web.Client.Models.Shows.Params;
using FMFT.Web.Client.Models.Shows;
using FMFT.Web.Client.Services.Views.Shows;
using FMFT.Web.Client.Views.Bases.Alerts;
using FMFT.Web.Client.Views.Bases.Buttons;
using FMFT.Web.Client.Views.Bases.Forms;
using FMFT.Web.Client.Views.Bases.Inputs;
using Microsoft.AspNetCore.Components;
using FMFT.Web.Shared.Extensions;

namespace FMFT.Web.Client.Views.Components.Shows.Forms
{
    public partial class AddShowForm
    {
        [Parameter]
        public IEnumerable<Auditorium> Auditoriums { get; set; }

        [Inject]
        public IShowViewService ShowViewService { get; set; }

        public AddShowParams Params { get; set; }

        public AlertGroupBase AlertGroup { get; set; }
        public AlertBase AuditoriumNotFoundAlert { get; set; }
        public AlertBase ValidationAlert { get; set; }
        public AlertBase SuccessAlert { get; set; }

        public FormBase Form { get; set; }
        public TextInputBase PublicIdInput { get; set; }
        public TextInputBase NameInput { get; set; }
        public TextareaInputBase DescriptionInput { get; set; }
        public ButtonBase SubmitButton { get; set; }

        private string calendarStartDate = DateTime.Now.TruncateToMinuteStart().ToString("s");
        private string calendarEndDate = DateTime.Now.AddMonths(12).TruncateToMinuteStart().ToString("s");

        protected override void OnParametersSet()
        {
            Params = new AddShowParams()
            {
                StartDateTime = DateTimeOffset.Now.AddHours(1).TruncateToMinuteStart(),
                EndDateTime = DateTimeOffset.Now.AddHours(3).TruncateToMinuteStart(),
                AuditoriumId = Auditoriums?.FirstOrDefault()?.Id ?? 0
            };
        }

        public async Task SubmitAddShowAsync()
        {
            Form.ClearValidations();
            Form.DisableAll();
            AlertGroup.HideAll();
            SubmitButton.StartSpinning();

            try
            {
                Show show = await ShowViewService.AddShowAsync(Params);
                SuccessAlert.Show();
                ShowViewService.NavigateTo($"/admin/shows/{show.Id}");
            }
            catch (AuditoriumNotFoundException)
            {
                AuditoriumNotFoundAlert.Show();
            }
            catch (AddShowValidationException exception)
            {
                ValidationAlert.Show();
                Form.HandleValidationXeption(exception);
            }

            Form.EnableAll();
            SubmitButton.StopSpinning();
        }
    }
}

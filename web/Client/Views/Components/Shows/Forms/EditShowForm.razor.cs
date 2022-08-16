using FMFT.Web.Client.Models.Auditoriums;
using FMFT.Web.Client.Models.Auditoriums.Exceptions;
using FMFT.Web.Client.Models.Shows;
using FMFT.Web.Client.Models.Shows.Exceptions;
using FMFT.Web.Client.Models.Shows.Params;
using FMFT.Web.Client.Services.Views.Accounts;
using FMFT.Web.Client.Services.Views.Shows;
using FMFT.Web.Client.Views.Bases.Alerts;
using FMFT.Web.Client.Views.Bases.Buttons;
using FMFT.Web.Client.Views.Bases.Forms;
using FMFT.Web.Client.Views.Bases.Inputs;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Components.Shows.Forms
{
    public partial class EditShowForm
    {
        [Parameter]
        public Show Show { get; set; }

        [Parameter]
        public IEnumerable<Auditorium> Auditoriums { get; set; }

        [Inject]
        public IShowViewService ShowViewService { get; set; }

        public UpdateShowParams Params { get; set; }

        public AlertGroupBase AlertGroup { get; set; }
        public AlertBase AuditoriumNotFoundAlert { get; set; }
        public AlertBase ShowNotFoundAlert { get; set; }
        public AlertBase ValidationAlert { get; set; }
        public AlertBase SuccessAlert { get; set; }

        public FormBase Form { get; set; }
        public TextInputBase PublicIdInput { get; set; }
        public TextInputBase NameInput { get; set; }
        public TextareaInputBase DescriptionInput { get; set; }
        public ButtonBase SubmitButton { get; set; }

        protected override void OnParametersSet()
        {
            Params = new UpdateShowParams()
            {
                Id = Show.Id,
                PublicId = Show.PublicId,
                Name = Show.Name,
                Description = Show.Description,
                StartDateTime = Show.StartDateTime,
                EndDateTime = Show.EndDateTime,
                AuditoriumId = Show.AuditoriumId
            };
        }

        public async Task SubmitUpdateShowAsync()
        {
            Form.DisableAll();
            AlertGroup.HideAll();
            SubmitButton.StartSpinning();

            try
            {
                await ShowViewService.UpdateShowAsync(Params);
                SuccessAlert.Show();
            } catch (AuditoriumNotFoundException)
            {
                AuditoriumNotFoundAlert.Show();
            } catch (ShowNotFoundException)
            {
                AuditoriumNotFoundAlert.Show();
            } catch (UpdateShowValidationException)
            {
                ValidationAlert.Show();
            }

            Form.EnableAll();
            SubmitButton.StopSpinning();            
        }
    }
}

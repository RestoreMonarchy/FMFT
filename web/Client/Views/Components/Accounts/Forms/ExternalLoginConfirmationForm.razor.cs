using FMFT.Web.Client.Services.Views.Accounts;
using FMFT.Web.Client.Views.Bases.Alerts;
using FMFT.Web.Client.Views.Bases.Buttons;
using FMFT.Web.Client.Views.Bases.Forms;
using FMFT.Web.Client.Views.Bases.Inputs;
using FMFT.Web.Shared.Models.Users.Exceptions;
using FMFT.Web.Shared.Models.Users.Models;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Components.Accounts.Forms
{
    public partial class ExternalLoginConfirmationForm
    {
        [Inject]
        public IAccountViewService AccountViewService { get; set; }

        public ExternalLoginConfirmationModel Model { get; set; } = new();

        public AlertGroupBase AlertGroup { get; set; }
        public AlertBase UserEmailAlreadyExistsAlert { get; set; }
        public AlertBase ExternalLoginNotFoundAlert { get; set; }

        public FormBase Form { get; set; }
        public TextInputBase EmailInput { get; set; }
        public ButtonBase SubmitButton { get; set; }

        public async Task SubmitConfirmationAsync()
        {
            AlertGroup.HideAll();
            Form.DisableAll();
            SubmitButton.StartSpinning();

            try
            {
                await Task.Delay(2000);
                await AccountViewService.ConfirmExternalLoginAsync(Model);
                AccountViewService.ForceLoadNavigateTo("/");
            }
            catch (UserEmailAlreadyExistsException)
            {
                UserEmailAlreadyExistsAlert.Show();
            } catch (ExternalLoginInfoNotFoundException)
            {
                ExternalLoginNotFoundAlert.Show();
            }
            finally
            {
                SubmitButton.StopSpinning();
                Form.EnableAll();
            }
        }
    }
}

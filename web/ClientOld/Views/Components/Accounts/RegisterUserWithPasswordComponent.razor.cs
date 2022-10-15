using FMFT.Web.Client.Models.Accounts.Exceptions;
using FMFT.Web.Client.Models.Accounts.Requests;
using FMFT.Web.Client.Services.Views.Accounts;
using FMFT.Web.Client.Views.Bases.Alerts;
using FMFT.Web.Client.Views.Bases.Buttons;
using FMFT.Web.Client.Views.Bases.Forms;
using FMFT.Web.Client.Views.Bases.Inputs;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Components.Accounts
{
    public partial class RegisterUserWithPasswordComponent
    {
        [Inject]
        public IAccountViewService AccountViewService { get; set; }

        public RegisterWithPasswordRequest Request { get; set; } = new();

        public FormBase Form { get; set; }
        public ButtonBase SubmitButton { get; set; }
        public TextInputBase EmailInput { get; set; }
        public TextInputBase PasswordInput { get; set; }
        public TextInputBase FirstNameInput { get; set; }
        public TextInputBase LastNameInput { get; set; }

        public AlertGroupBase AlertGroup { get; set; }
        public AlertBase ConflictErrorAlert { get; set; }
        public AlertBase BadRequestErrorAlert { get; set; }

        public async Task SubmitRegisterAsync()
        {
            AlertGroup.HideAll();
            Form.ClearValidations();
            Form.DisableAll();            
            SubmitButton.StartSpinning();

            try
            {
                await AccountViewService.RegisterAsync(Request);
                AccountViewService.ForceLoadNavigateTo("/");
            } catch (AccountEmailAlreadyExistsException)
            {
                ConflictErrorAlert.Show();
            } catch (AccountRegisterWithPasswordValidationException exception)
            {
                Form.HandleValidationXeption(exception);
                BadRequestErrorAlert.Show();
            } finally
            {
                SubmitButton.StopSpinning();
                Form.EnableAll();
            } 
        }
    }
}

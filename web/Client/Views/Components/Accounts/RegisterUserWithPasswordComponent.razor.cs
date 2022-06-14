using FMFT.Web.Client.Services.Views.Accounts;
using FMFT.Web.Client.Views.Bases.Alerts;
using FMFT.Web.Client.Views.Bases.Buttons;
using FMFT.Web.Client.Views.Bases.Forms;
using FMFT.Web.Client.Views.Bases.Inputs;
using FMFT.Web.Shared.Models.Users.Exceptions;
using FMFT.Web.Shared.Models.Users.Models;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Components.Accounts
{
    public partial class RegisterUserWithPasswordComponent
    {
        [Inject]
        public IAccountViewService AccountViewService { get; set; }

        public RegisterUserWithPasswordModel Model { get; set; } = new();

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
            Form.DisableAll();
            SubmitButton.StartSpinning();
            try
            {
                await Task.Delay(2000);
                await AccountViewService.RegisterAsync(Model);
                AccountViewService.ForceLoadNavigateTo("/");
            } catch (UserEmailAlreadyExistsException)
            {
                ConflictErrorAlert.Show();
            } catch (RegisterUserWithPasswordValidationException exception)
            {
                BadRequestErrorAlert.Show();
            } finally
            {
                SubmitButton.StopSpinning();
                Form.EnableAll();
            } 
        }
    }
}

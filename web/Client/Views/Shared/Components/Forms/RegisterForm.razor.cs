using FMFT.Extensions.Blazor.Bases.Alerts;
using FMFT.Extensions.Blazor.Bases.Buttons;
using FMFT.Extensions.Blazor.Bases.Forms;
using FMFT.Extensions.Blazor.Bases.Inputs;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Accounts;
using FMFT.Web.Client.Models.API.Accounts.Requests;
using FMFT.Web.Client.Services.Accounts;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Shared.Components.Forms
{
    public partial class RegisterForm
    {
        [Inject]
        public IAccountService AccountService { get; set; }

        [Parameter]
        public EventCallback<AccountToken> OnSuccess { get; set; }

        public RegisterWithPasswordRequest Model { get; set; } = new();

        public FormBase Form { get; set; }
        public SubmitButtonBase SubmitButton { get; set; }
        public TextInputBase EmailInput { get; set; }
        public TextInputBase PasswordInput { get; set; }
        public TextInputBase FirstNameInput { get; set; }
        public TextInputBase LastNameInput { get; set; }

        public AlertGroupBase AlertGroup { get; set; }
        public AlertBase UserAlreadyExistsAlert { get; set; }
        public AlertBase ValidationErrorAlert { get; set; }
        public AlertBase SuccessAlert { get; set; }        

        public async Task SubmitAsync()
        {
            AlertGroup.HideAll();
            Form.ClearValidations();
            Form.DisableAll();
            SubmitButton.StartSpinning();

            APIResponse<AccountToken> response = await APIBroker.PostAccountRegisterAsync(Model);

            if (response.IsSuccessfull)
            {
                SuccessAlert.Show();
                await OnSuccess.InvokeAsync(response.Object);
            } else
            {
                if (response.Error.Code == "ERR005")
                {
                    Form.HandleErrors(response.Error.Data);
                    ValidationErrorAlert.Show();
                } else if (response.Error.Code == "ERR006")
                {
                    UserAlreadyExistsAlert.Show();
                }
            }

            SubmitButton.StopSpinning();
            Form.EnableAll();
        }
    }
}

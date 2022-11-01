using FMFT.Extensions.Blazor.Bases.Alerts;
using FMFT.Extensions.Blazor.Bases.Buttons;
using FMFT.Extensions.Blazor.Bases.Forms;
using FMFT.Extensions.Blazor.Bases.Inputs;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.ResetPasswordRequests.Requests;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Shared.Components.Forms
{
    public partial class ResetPasswordForm
    {
        [Parameter]
        public Guid SecretKey { get; set; }
        [Parameter]
        public EventCallback OnSuccess { get; set; }

        public FormBase Form { get; set; }
        public SubmitButtonBase SubmitButton { get; set; }
        public TextInputBase PasswordInput { get; set; }

        public AlertGroupBase AlertGroup { get; set; }
        public AlertBase ValidationErrorAlert { get; set; }
        public AlertBase NotFoundErrorAlert { get; set; }
        public AlertBase AlreadyUsedErrorAlert { get; set; }
        public AlertBase ExpiredErrorAlert { get; set; }
        public AlertBase SuccessAlert { get; set; }

        public string NewPassword { get; set; }

        private async Task SubmitAsync()
        {
            AlertGroup.HideAll();
            Form.DisableAll();
            SubmitButton.StartSpinning();

            ResetPasswordRequest request = new()
            {
                SecretKey = SecretKey,
                NewPassword = NewPassword
            };

            APIResponse response = await APIBroker.ResetPasswordAsync(request);

            if (response.IsSuccessfull)
            {
                NewPassword = null;
                SuccessAlert.Show();
                await OnSuccess.InvokeAsync();
            }
            else
            {
                switch (response.Error.Code)
                {
                    case "ERR029":
                        NotFoundErrorAlert.Show();
                        break;
                    case "ERR027":
                        ExpiredErrorAlert.Show();
                        break;
                    case "ERR026":
                        AlreadyUsedErrorAlert.Show();
                        break;
                    case "ERR025":
                        ValidationErrorAlert.Show();
                        break;
                }
            }

            SubmitButton.StopSpinning();
            Form.EnableAll();
        }
    }
}

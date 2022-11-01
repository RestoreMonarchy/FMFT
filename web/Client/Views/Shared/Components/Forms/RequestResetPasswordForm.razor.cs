using FMFT.Extensions.Blazor.Bases.Alerts;
using FMFT.Extensions.Blazor.Bases.Buttons;
using FMFT.Extensions.Blazor.Bases.Forms;
using FMFT.Extensions.Blazor.Bases.Inputs;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.ResetPasswordRequests.Requests;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Shared.Components.Forms
{
    public partial class RequestResetPasswordForm
    {
        [Parameter]
        public EventCallback OnSuccess { get; set; }

        public FormBase Form { get; set; }
        public SubmitButtonBase SubmitButton { get; set; }
        public TextInputBase EmailInput { get; set; }

        public AlertGroupBase AlertGroup { get; set; }
        public AlertBase LimitReachedAlert { get; set; }
        public AlertBase SuccessAlert { get; set; }

        public CreateResetPasswordRequestRequest Model { get; set; } = new();

        private async Task SubmitAsync()
        {
            AlertGroup.HideAll();
            Form.DisableAll();
            SubmitButton.StartSpinning();

            APIResponse response = await APIBroker.CreateResetPasswordRequestAsync(Model);

            if (response.IsSuccessfull)
            {
                Model = new();
                SuccessAlert.Show();
                await OnSuccess.InvokeAsync();
            }
            else
            {
                if (response.Error.Code == "ERR028")
                {
                    LimitReachedAlert.Show();
                }
            }

            SubmitButton.StopSpinning();
            Form.EnableAll();
        }
    }
}

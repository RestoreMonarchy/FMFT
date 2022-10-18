using FMFT.Extensions.Blazor.Bases.Alerts;
using FMFT.Extensions.Blazor.Bases.Buttons;
using FMFT.Extensions.Blazor.Bases.Forms;
using FMFT.Extensions.Blazor.Bases.Inputs;
using FMFT.Web.Client.Brokers.APIs;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Accounts;
using FMFT.Web.Client.Models.API.Accounts.Requests;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Shared.Components.Forms
{
    public partial class LoginForm
    {
        [Inject]
        public IAPIBroker APIBroker { get; set; }

        public FormBase Form { get; set; }
        public ButtonBase SubmitButton { get; set; }
        public TextInputBase EmailInput { get; set; }
        public TextInputBase PasswordInput { get; set; }
        public CheckboxInputBase PersistentCheckbox { get; set; }

        public AlertGroupBase AlertGroup { get; set; }
        public AlertBase PasswordNotMatchAlert { get; set; }
        public AlertBase SuccessAlert { get; set; }

        public LogInWithPasswordRequest Model { get; set; } = new();

        private async Task SubmitAsync()
        {
            AlertGroup.HideAll();
            Form.DisableAll();
            SubmitButton.StartSpinning();

            APIResponse<AccountToken> response = await APIBroker.PostAccountLoginAsync(Model);

            if (response.IsSuccessfull)
            {
                SuccessAlert.Show();
            } else
            {
                if (response.Error.Code == "ERR009")
                {
                    PasswordNotMatchAlert.Show();
                }
            }

            SubmitButton.StopSpinning();
            Form.EnableAll();
        }
    }
}

using FMFT.Extensions.Blazor.Bases.Alerts;
using FMFT.Extensions.Blazor.Bases.Buttons;
using FMFT.Extensions.Blazor.Bases.Dialogs;
using FMFT.Web.Client.Models.API;

namespace FMFT.Web.Client.Views.Shared.Components.Dialogs
{
    public partial class SendConfirmEmailDialog
    {
        public ModalDialog ModalDialog { get; set; }
        public ButtonBase SendButton { get; set; }
        public AlertGroupBase AlertGroup { get; set; }
        public AlertBase LimitAlert { get; set; }
        public AlertBase AlreadyConfirmedAlert { get; set; }
        public AlertBase ErrorAlert { get; set; }
        public AlertBase SuccessAlert { get; set; }


        public async Task ShowAsync()
        {
            await ModalDialog.ShowAsync();
        }

        private async Task HandleSendAsync()
        {
            AlertGroup.HideAll();
            SendButton.StartSpinning();

            APIResponse response = await APIBroker.SendConfirmUserEmailAsync(UserAccountState.UserAccount.UserId);

            if (response.IsSuccessful)
            {
                SuccessAlert.Show();
            } else
            {
                switch (response.Error.Code)
                {
                    case "ERR021":
                        AlreadyConfirmedAlert.Show();
                        break;
                    case "ERR039":
                        LimitAlert.Show();
                        break;
                    default:
                        ErrorAlert.Show();
                        break;
                }
            }

            SendButton.StopSpinning();
        }
    }
}

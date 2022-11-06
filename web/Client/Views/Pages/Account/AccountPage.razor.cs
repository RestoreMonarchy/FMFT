using FMFT.Extensions.Blazor.Bases.Alerts;
using FMFT.Extensions.Blazor.Bases.Buttons;
using FMFT.Extensions.Blazor.Bases.Dialogs;
using FMFT.Extensions.Blazor.Bases.Inputs;
using FMFT.Extensions.Blazor.Bases.Loadings;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Accounts.Requests;
using FMFT.Web.Client.Models.API.Users;
using FMFT.Web.Client.StateContainers.UserAccounts;
using System.ComponentModel;

namespace FMFT.Web.Client.Views.Pages.Account
{
    public partial class AccountPage
    {
        public ModalDialog ChangePasswordDialog { get; set; }
        public AlertGroupBase ChangePasswordAlertGroup { get; set; }
        public AlertBase ChangePasswordRequiredInputAlert { get; set; }
        public AlertBase ChangePasswordInvalidCredentialsAlert { get; set; }
        public AlertBase ChangePasswordNotEnabledPasswordAlert { get; set; }
        public AlertBase ChangePasswordValidationErrorAlert { get; set; }
        public PasswordInputBase CurrentPasswordInput { get; set; }
        public PasswordInputBase NewPasswordInput { get; set; }
        public ButtonBase ChangePasswordButton { get; set; }
        public LoadingView LoginsLoadingView { get; set; }

        public ChangePasswordRequest ChangePasswordRequest { get; set; } = new();

        public List<UserLogin> UserLogins => UserLoginsResponse.Object;

        public APIResponse<List<UserLogin>> UserLoginsResponse { get; set; }
        public APIResponse ChangePasswordResponse { get; set; }
        private bool showResult = false;

        protected override async Task OnInitializedAsync()
        {
            if (UserAccountState.IsAuthenticated && !UserAccountState.UserAccount.IsPasswordEnabled)
            {
                UserLoginsResponse = await APIBroker.GetUserLoginsByUserIdAsync(UserAccountState.UserAccount.UserId);
                LoginsLoadingView.StopLoading();
            }
        }

        private async Task HandleChangePasswordDialogAsync()
        {
            showResult = false;
            await ChangePasswordDialog.ShowStaticAsync();
            ChangePasswordRequest = new();
        }

        private async Task HandleChangePasswordAsync()
        {
            ChangePasswordAlertGroup.HideAll();

            if (string.IsNullOrEmpty(ChangePasswordRequest.CurrentPasswordText) || string.IsNullOrEmpty(ChangePasswordRequest.PasswordText))
            {
                ChangePasswordRequiredInputAlert.Show();
                return;
            }

            ChangePasswordButton.StartSpinning();

            ChangePasswordResponse = await APIBroker.PostAccountChangePasswordAsync(ChangePasswordRequest);

            if (!ChangePasswordResponse.IsSuccessfull)
            {
                switch (ChangePasswordResponse.Error.Code)
                {
                    case "ERR023":
                        ChangePasswordNotEnabledPasswordAlert.Show();
                        break;
                    case "ERR024":
                        ChangePasswordValidationErrorAlert.Show();
                        break;
                    case "ERR009":
                        ChangePasswordInvalidCredentialsAlert.Show();
                        break;
                }
            } else
            {
                showResult = true;
            }

            ChangePasswordButton.StopSpinning();            
        }
    }
}

using FMFT.Extensions.Blazor.Bases.Alerts;
using FMFT.Extensions.Blazor.Bases.Buttons;
using FMFT.Extensions.Blazor.Bases.Dialogs;
using FMFT.Extensions.Blazor.Bases.Inputs;
using FMFT.Extensions.Blazor.Bases.Loadings;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Users;
using FMFT.Web.Client.Models.API.Users.Requests;
using FMFT.Web.Shared.Enums;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Pages.Admin.Users
{
    public partial class UserAdminPage
    {
        [Parameter]
        public int UserId { get; set; }

        public string UserName => UserResponse?.Object?.Email ?? UserId.ToString();

        public LoadingView LoadingView { get; set; }
        public LoadingView UserLoginsLoadingView { get; set; }

        public APIResponse<User> UserResponse { get; set; }
        public APIResponse<List<UserLogin>> UserLoginsResponse { get; set; }

        public User User => UserResponse.Object;
        public List<UserLogin> UserLogins => UserLoginsResponse.Object;

        protected override async Task OnParametersSetAsync()
        {
            if (!UserAccountState.IsInRole(UserRole.Admin))
            {
                return;
            }

            UserResponse = await APIBroker.GetUserByIdAsync(UserId);
            LoadingView.StopLoading();

            if (!UserResponse.IsSuccessful)
            {
                return;
            }

            changeRole = User.Role;

            if (!User.IsPasswordEnabled)
            {
                UserLoginsResponse = await APIBroker.GetUserLoginsByUserIdAsync(UserId);
                UserLoginsLoadingView.StopLoading();
            }
        }

        public ModalDialog ChangeRoleModalDialog { get; set; }
        public ButtonBase ChangeRoleButton { get; set; }
        public SelectEnumBase<UserRole> ChangeRoleSelect { get; set; }
        public AlertGroupBase ChangeRoleAlertGroup { get; set; }
        public AlertBase ChangeRoleSuccessAlert { get; set; }
        public AlertBase ChangeRoleErrorAlert { get; set; }
        public AlertBase ChangeRoleSameAlert { get; set; }        

        private UserRole changeRole;

        private async Task HandleOpenChangeRoleAsync()
        {
            ChangeRoleAlertGroup.HideAll();
            await ChangeRoleModalDialog.ShowAsync();
        }

        private async Task HandleChangeRoleAsync()
        {
            ChangeRoleAlertGroup.HideAll();
            ChangeRoleSelect.Disable();
            ChangeRoleButton.StartSpinning();

            UpdateUserRoleRequest request = new()
            {
                UserId = UserId,
                Role = changeRole
            };

            APIResponse response = await APIBroker.UpdateUserRoleAsync(request);

            ChangeRoleButton.StopSpinning();

            if (response.IsSuccessful)
            {
                ChangeRoleSuccessAlert.Show();
                User.Role = changeRole;
            }
            else
            {
                switch (response.Error.Code)
                {
                    case "ERR010":
                        ChangeRoleSameAlert.Show();
                        break;
                    default:
                        ChangeRoleErrorAlert.Show();
                        break;
                }                
            }

            ChangeRoleSelect.Enable();
        }
    }
}
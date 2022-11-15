using FMFT.Extensions.Blazor.Bases.Loadings;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Users;
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

            if (!User.IsPasswordEnabled)
            {
                UserLoginsResponse = await APIBroker.GetUserLoginsByUserIdAsync(UserId);
                UserLoginsLoadingView.StopLoading();
            }
        }

    }
}

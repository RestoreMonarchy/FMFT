using FMFT.Web.Client.Models.Auditoriums.Exceptions;
using FMFT.Web.Client.Models.Shows.Exceptions;
using FMFT.Web.Client.Models.Shows;
using FMFT.Web.Client.Models.Users;
using FMFT.Web.Client.Models.Users.Requests;
using FMFT.Web.Client.Services.Views.Shows;
using FMFT.Web.Client.Services.Views.Users;
using FMFT.Web.Client.Views.Bases.Alerts;
using FMFT.Web.Client.Views.Bases.Buttons;
using FMFT.Web.Client.Views.Bases.Forms;
using FMFT.Web.Client.Views.Bases.Inputs;
using FMFT.Web.Shared.Enums;
using Microsoft.AspNetCore.Components;
using FMFT.Web.Client.Models.Users.Exceptions;

namespace FMFT.Web.Client.Views.Components.Users.Forms
{
    public partial class UserRoleForm
    {
        [Inject]
        public IUserViewService UserViewService { get; set; }

        [Parameter]
        public User User { get; set; }

        public UpdateUserRoleRequest Request { get; set; }

        protected override void OnParametersSet()
        {
            Request = new UpdateUserRoleRequest()
            {
                UserId = User.Id,
                Role = User.Role
            };
        }

        public AlertGroupBase AlertGroup { get; set; }
        public AlertBase UserNotFoundAlert { get; set; }
        public AlertBase UserRoleAlreadyExistsAlert { get; set; }
        public AlertBase SuccessAlert { get; set; }
        public FormBase Form { get; set; }
        public SelectEnumBase<UserRole> RoleSelect { get; set; }
        public ButtonBase SubmitButton { get; set; }

        public async Task SubmitUpdateRoleAsync()
        {
            Form.DisableAll();
            AlertGroup.HideAll();
            SubmitButton.StartSpinning();

            try
            {
                await UserViewService.UpdateUserRoleAsync(Request);
                User.Role = Request.Role;
                SuccessAlert.Show();
            }
            catch (UserNotFoundException)
            {
                UserNotFoundAlert.Show();
            }
            catch (UserRoleAlreadyExistsException)
            {
                UserRoleAlreadyExistsAlert.Show();
            }

            Form.EnableAll();
            SubmitButton.StopSpinning();
        }
    }
}

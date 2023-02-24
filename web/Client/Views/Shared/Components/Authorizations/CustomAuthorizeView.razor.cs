using FMFT.Web.Client.Models.API.Accounts;
using FMFT.Web.Shared.Enums;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;

namespace FMFT.Web.Client.Views.Shared.Components.Authorizations
{
    public partial class CustomAuthorizeView
    {
        [Parameter]
        public RenderFragment<UserAccount> Authorized { get; set; }
        [Parameter]
        public RenderFragment NotAuthorized { get; set; }
        [Parameter]
        public RenderFragment<UserAccount> NotConfirmedEmail { get; set; }
        [Parameter]
        public IEnumerable<UserRole> Roles { get; set; }
        [Parameter]
        public UserRole? Role { get; set; }
        [Parameter]
        public bool MustConfirmEmail { get; set; }

        protected override void OnInitialized()
        {
            UserAccountState.OnChange += StateHasChanged;
        }

        private IEnumerable<UserRole> roles
        {
            get
            {
                if (Roles == null && Role == null)
                    return null;

                List<UserRole> roles = Roles?.ToList() ?? new List<UserRole>();

                if (Role.HasValue)
                    roles.Add(Role.Value);

                return roles;
            }
        }

        public bool IsConfirmedEmail()
        {
            if (MustConfirmEmail && !UserAccountState.IsEmailConfirmed)
            {
                return false;
            }

            return true;
        }

        public bool IsAuthorized()
        {
            if (!UserAccountState.IsAuthenticated)
            {
                return false;
            }

            if (roles != null && !roles.Contains(UserAccountState.UserAccount.Role))
            {
                return false;
            }

            return true;
        }
    }
}

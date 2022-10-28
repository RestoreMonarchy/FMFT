using FMFT.Web.Client.Models.API.Accounts;
using FMFT.Web.Shared.Enums;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Shared.Components.Authorizations
{
    public partial class CustomAuthorizeView
    {
        [Parameter]
        public RenderFragment<UserAccount> Authorized { get; set; }
        [Parameter]
        public RenderFragment NotAuthorized { get; set; }
        [Parameter]
        public IEnumerable<UserRole> Roles { get; set; }
        [Parameter]
        public UserRole? Role { get; set; }

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

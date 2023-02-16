using FMFT.Extensions.Blazor.Bases.Loadings;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Users;
using FMFT.Web.Shared.Enums;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Pages.Admin.Users
{
    public partial class UsersAdminPage
    {
        public LoadingView LoadingView { get; set; }

        public APIResponse<List<User>> UsersResponse { get; set; }

        public List<User> Users => UsersResponse.Object;

        private IEnumerable<User> SearchUsers => FilterUsers.Where(x => string.IsNullOrEmpty(searchString) 
            || x.Email.Contains(searchString, StringComparison.OrdinalIgnoreCase)
            || (x.FirstName != null && x.FirstName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            || (x.LastName != null && x.LastName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            || x.FullName().Contains(searchString, StringComparison.OrdinalIgnoreCase));

        private IEnumerable<User> FilterUsers => Users
            .Where(x => RoleFilters[x.Role])
            .OrderByDescending(x => x.CreateDate);

        private Dictionary<UserRole, bool> RoleFilters = new()
        {
            { UserRole.Guest, true },
            { UserRole.Admin, true }
        };

        private string searchString = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            if (!UserAccountState.IsInRole(UserRole.Admin))
            {
                return;
            }

            UsersResponse = await APIBroker.GetAllUsersAsync();

            LoadingView.StopLoading();
        }

        private string RoleFilterId(UserRole role)
        {
            return $"rolefilter-{role}";
        }

        private void ChangeRoleFilter(UserRole role, ChangeEventArgs args)
        {
            bool value = bool.Parse(args.Value.ToString());
            RoleFilters[role] = value;
            StateHasChanged();
        }
    }
}
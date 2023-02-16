using FMFT.Extensions.Blazor.Bases.Loadings;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Users;
using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Client.Views.Pages.Admin.Users
{
    public partial class UsersAdminPage
    {
        public LoadingView LoadingView { get; set; }

        public APIResponse<List<User>> UsersResponse { get; set; }

        public List<User> Users => UsersResponse.Object;

        private IEnumerable<User> SearchUsers => Users.Where(x => string.IsNullOrEmpty(searchString) 
            || x.Email.Contains(searchString, StringComparison.OrdinalIgnoreCase)
            || (x.FirstName != null && x.FirstName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            || (x.LastName != null && x.LastName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            || x.FullName().Contains(searchString, StringComparison.OrdinalIgnoreCase));

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
    }
}
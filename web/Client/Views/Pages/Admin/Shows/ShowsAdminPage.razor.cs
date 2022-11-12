using FMFT.Extensions.Blazor.Bases.Loadings;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Shows;
using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Client.Views.Pages.Admin.Shows
{
    public partial class ShowsAdminPage
    {
        public LoadingView LoadingView { get; set; }

        public List<Show> Shows => ShowsResponse.Object;

        public APIResponse<List<Show>> ShowsResponse { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (!UserAccountState.IsInRole(UserRole.Admin))
            {
                return;
            }

            ShowsResponse = await APIBroker.GetAllShowsAsync();
            LoadingView.StopLoading();
        }
    }
}

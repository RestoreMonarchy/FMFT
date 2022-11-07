using FMFT.Extensions.Blazor.Bases.Loadings;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Shows;

namespace FMFT.Web.Client.Views.Pages.Admin.Shows
{
    public partial class ShowsAdminPage
    {
        public LoadingView LoadingView { get; set; }

        public List<Show> Shows => ShowsResponse.Object;

        public APIResponse<List<Show>> ShowsResponse { get; set; }

        protected override async Task OnInitializedAsync()
        {
            ShowsResponse = await APIBroker.GetAllShowsAsync();
            LoadingView.StopLoading();
        }
    }
}

using FMFT.Extensions.Blazor.Bases.Loadings;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Shows;

namespace FMFT.Web.Client.Views.Pages.Home
{
    public partial class IndexPage
    {
        public LoadingView ShowsLoadingView { get; set; }

        public APIResponse<List<Show>> ShowsResponse { get; set; }

        public List<Show> Shows => ShowsResponse.Object;

        protected override async Task OnInitializedAsync()
        {
            ShowsResponse = await APIBroker.GetPublicShowsAsync();
            ShowsLoadingView.StopLoading();
        }
    }
}

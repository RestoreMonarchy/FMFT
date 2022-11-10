using FMFT.Extensions.Blazor.Bases.Loadings;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Shows;
using static System.Net.Mime.MediaTypeNames;

namespace FMFT.Web.Client.Views.Pages.Home.Shows
{
    public partial class ShowsPage
    {
        public LoadingView LoadingView { get; set; }

        public APIResponse<List<Show>> ShowsResponse { get; set; }

        public List<Show> Shows => ShowsResponse.Object;

        protected override async Task OnInitializedAsync()
        {
            ShowsResponse = await APIBroker.GetAllShowsAsync();
            LoadingView.StopLoading();
        }

        private string GetShowThumbnailStyle(Show show)
        {            
            if (!show.ThumbnailMediaId.HasValue)
            {
                return string.Empty;
            }

            string styleFormat = "background-image: linear-gradient(rgba(0, 0, 0, 0.6), rgba(0, 0, 0, 0.6)), url({0});";
            string thumbnailUrl = MediaService.GetMediaUrl(show.ThumbnailMediaId.Value);

            return string.Format(styleFormat, thumbnailUrl);
        }
    }
}

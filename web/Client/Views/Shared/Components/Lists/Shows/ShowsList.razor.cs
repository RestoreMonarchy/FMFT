using FMFT.Web.Client.Models.API.Shows;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Shared.Components.Lists.Shows
{
    public partial class ShowsList
    {
        [Parameter]
        public List<Show> Shows { get; set; }

        public IEnumerable<Show> FutureShows => Shows.Where(x => !x.IsPast());
        public IEnumerable<Show> PastShows => Shows.Where(x => x.IsPast());

        private string GetShowThumbnailStyle(Show show)
        {
            if (!show.ThumbnailMediaId.HasValue)
            {
                return string.Empty;
            }

            string styleFormat = "background-image: linear-gradient(rgba(0, 0, 0, 0.45), rgba(0, 0, 0, 0.45)), url({0});";
            string thumbnailUrl = MediaService.GetMediaUrl(show.ThumbnailMediaId.Value);

            return string.Format(styleFormat, thumbnailUrl);
        }
    }
}

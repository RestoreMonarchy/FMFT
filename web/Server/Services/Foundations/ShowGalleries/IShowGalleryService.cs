using FMFT.Web.Server.Models.ShowGalleries;
using FMFT.Web.Server.Models.ShowGalleries.Params;

namespace FMFT.Web.Server.Services.Foundations.ShowGalleries
{
    public interface IShowGalleryService
    {
        ValueTask<ShowGallery> AddShowGalleryAsync(AddShowGalleryParams @params);
        ValueTask DeleteShowGalleryByIdAsync(int showGalleryId);
        ValueTask<IEnumerable<ShowGallery>> RetrieveShowGalleryByShowIdAsync(int showId);
    }
}
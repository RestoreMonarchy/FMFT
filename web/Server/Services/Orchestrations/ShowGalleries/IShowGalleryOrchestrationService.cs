using FMFT.Web.Server.Models.ShowGalleries;
using FMFT.Web.Server.Models.ShowGalleries.Params;

namespace FMFT.Web.Server.Services.Orchestrations.ShowGalleries
{
    public interface IShowGalleryOrchestrationService
    {
        ValueTask<ShowGallery> AddShowGalleryAsync(AddShowGalleryParams @params);
        ValueTask DeleteShowGalleryByIdAsync(int showGalleryId);
        ValueTask<IEnumerable<ShowGallery>> RetrieveShowGalleryByShowIdAsync(int showId);
    }
}
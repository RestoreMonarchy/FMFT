using FMFT.Web.Server.Models.ShowGalleries;
using FMFT.Web.Server.Models.ShowGalleries.DTOs;

namespace FMFT.Web.Server.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<IEnumerable<ShowGallery>> SelectShowGalleryByShowIdAsync(int showId);
        ValueTask DeleteShowGalleryByIdAsync(int showGalleryId);
        ValueTask<ShowGallery> InsertShowGalleryAsync(InsertShowGalleryDTO dto);
    }
}

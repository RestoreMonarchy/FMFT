using FMFT.Web.Server.Brokers.Storages;
using FMFT.Web.Server.Models.ShowGalleries;
using FMFT.Web.Server.Models.ShowGalleries.DTOs;
using FMFT.Web.Server.Models.ShowGalleries.Params;

namespace FMFT.Web.Server.Services.Foundations.ShowGalleries
{
    public class ShowGalleryService : IShowGalleryService
    {
        private readonly IStorageBroker storageBroker;

        public ShowGalleryService(IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;
        }

        public async ValueTask<ShowGallery> AddShowGalleryAsync(AddShowGalleryParams @params)
        {
            InsertShowGalleryDTO dto = new()
            {
                ShowId = @params.ShowId,
                MediaId = @params.MediaId
            };

            return await storageBroker.InsertShowGalleryAsync(dto);
        }
        
        public async ValueTask<IEnumerable<ShowGallery>> RetrieveShowGalleryByShowIdAsync(int showId)
        {
            return await storageBroker.SelectShowGalleryByShowIdAsync(showId);
        }

        public async ValueTask DeleteShowGalleryByIdAsync(int showGalleryId)
        {
            await storageBroker.DeleteShowGalleryByIdAsync(showGalleryId);
        }
    }
}

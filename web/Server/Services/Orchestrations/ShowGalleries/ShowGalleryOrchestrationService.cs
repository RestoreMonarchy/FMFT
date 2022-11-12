using FMFT.Web.Server.Models.ShowGalleries;
using FMFT.Web.Server.Models.ShowGalleries.Params;
using FMFT.Web.Server.Services.Foundations.ShowGalleries;

namespace FMFT.Web.Server.Services.Orchestrations.ShowGalleries
{
    public class ShowGalleryOrchestrationService : IShowGalleryOrchestrationService
    {
        private readonly IShowGalleryService showGalleryService;

        public ShowGalleryOrchestrationService(IShowGalleryService showGalleryService)
        {
            this.showGalleryService = showGalleryService;
        }

        public async ValueTask<ShowGallery> AddShowGalleryAsync(AddShowGalleryParams @params)
        {
            return await showGalleryService.AddShowGalleryAsync(@params);
        }

        public async ValueTask<IEnumerable<ShowGallery>> RetrieveShowGalleryByShowIdAsync(int showId)
        {
            return await showGalleryService.RetrieveShowGalleryByShowIdAsync(showId);
        }

        public async ValueTask DeleteShowGalleryByIdAsync(int showGalleryId)
        {
            await showGalleryService.DeleteShowGalleryByIdAsync(showGalleryId);
        }
    }
}

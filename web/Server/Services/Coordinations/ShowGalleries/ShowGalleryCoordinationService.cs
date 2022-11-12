using FMFT.Web.Server.Models.ShowGalleries;
using FMFT.Web.Server.Models.ShowGalleries.Params;
using FMFT.Web.Server.Services.Orchestrations.ShowGalleries;
using FMFT.Web.Server.Services.Orchestrations.UserAccounts;
using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Server.Services.Coordinations.ShowGalleries
{
    public class ShowGalleryCoordinationService : IShowGalleryCoordinationService
    {
        private readonly IShowGalleryOrchestrationService showGalleryService;
        private readonly IUserAccountOrchestrationService userAccountService;

        public ShowGalleryCoordinationService(IShowGalleryOrchestrationService showGalleryService, IUserAccountOrchestrationService userAccountService)
        {
            this.showGalleryService = showGalleryService;
            this.userAccountService = userAccountService;
        }

        public async ValueTask<ShowGallery> AddShowGalleryAsync(AddShowGalleryParams @params)
        {
            await userAccountService.AuthorizeUserAccountByRoleAsync(UserRole.Admin);

            return await showGalleryService.AddShowGalleryAsync(@params);
        }

        public async ValueTask<IEnumerable<ShowGallery>> RetrieveShowGalleryByShowIdAsync(int showId)
        {
            return await showGalleryService.RetrieveShowGalleryByShowIdAsync(showId);
        }

        public async ValueTask DeleteShowGalleryByIdAsync(int showGalleryId)
        {
            await userAccountService.AuthorizeUserAccountByRoleAsync(UserRole.Admin);

            await showGalleryService.DeleteShowGalleryByIdAsync(showGalleryId);
        }
    }
}

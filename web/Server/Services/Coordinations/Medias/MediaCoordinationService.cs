using FMFT.Web.Server.Models.Medias;
using FMFT.Web.Server.Models.Medias.Params;
using FMFT.Web.Server.Services.Orchestrations.Medias;
using FMFT.Web.Server.Services.Orchestrations.UserAccounts;
using FMFT.Web.Shared.Enums;
using System.Runtime.CompilerServices;

namespace FMFT.Web.Server.Services.Coordinations.Medias
{
    public class MediaCoordinationService : IMediaCoordinationService
    {
        private readonly IMediaOrchestrationService mediaService;
        private readonly IUserAccountOrchestrationService userAccountService;

        public MediaCoordinationService(
            IMediaOrchestrationService mediaService, 
            IUserAccountOrchestrationService userAccountService)
        {
            this.mediaService = mediaService;
            this.userAccountService = userAccountService;
        }

        public async ValueTask<Media> AddMediaFromFormFileAsync(IFormFile formFile)
        {
            await userAccountService.AuthorizeUserAccountByRoleAsync(UserRole.Admin);

            return await mediaService.AddAccountMediaFromFormFileAsync(formFile);
        }

        public async ValueTask<IEnumerable<Media>> RetrieveAllMediaAsync()
        {
            await userAccountService.AuthorizeUserAccountByRoleAsync(UserRole.Admin);

            return await mediaService.RetrieveAllMediaAsync();
        }

        public async ValueTask<Media> RetrieveMediaByIdAsync(Guid mediaId)
        {
            return await mediaService.RetrieveMediaByIdAsync(mediaId);
        }
    }
}

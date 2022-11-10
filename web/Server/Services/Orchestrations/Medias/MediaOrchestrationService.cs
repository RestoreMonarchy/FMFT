using FMFT.Web.Server.Brokers.Converts;
using FMFT.Web.Server.Models.Accounts;
using FMFT.Web.Server.Models.Medias;
using FMFT.Web.Server.Models.Medias.Params;
using FMFT.Web.Server.Services.Foundations.Accounts;
using FMFT.Web.Server.Services.Foundations.Medias;

namespace FMFT.Web.Server.Services.Orchestrations.Medias
{
    public class MediaOrchestrationService : IMediaOrchestrationService
    {
        private readonly IMediaService mediaService;
        private readonly IAccountService accountService;

        public MediaOrchestrationService(
            IMediaService mediaService, 
            IAccountService accountService)
        {
            this.mediaService = mediaService;
            this.accountService = accountService;
        }

        public async ValueTask<Media> AddAccountMediaFromFormFileAsync(IFormFile formFile)
        {
            Account account = await accountService.RetrieveAccountAsync();
            int userId = account.UserId;

            return await mediaService.AddMediaFromFormFileAsync(formFile, userId);
        }

        public async ValueTask<IEnumerable<Media>> RetrieveAllMediaAsync()
        {
            return await mediaService.RetrieveAllMediaAsync();
        }
        
        public async ValueTask<Media> RetrieveMediaByIdAsync(Guid mediaId)
        {
            return await mediaService.RetrieveMediaByIdAsync(mediaId);
        }
    }
}

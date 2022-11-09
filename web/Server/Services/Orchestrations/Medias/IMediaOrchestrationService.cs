using FMFT.Web.Server.Models.Medias;
using FMFT.Web.Server.Models.Medias.Params;

namespace FMFT.Web.Server.Services.Orchestrations.Medias
{
    public interface IMediaOrchestrationService
    {
        ValueTask<Media> AddAccountMediaFromFormFileAsync(IFormFile formFile);
        ValueTask<Media> RetrieveMediaByIdAsync(Guid mediaId);
    }
}
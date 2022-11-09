using FMFT.Web.Server.Models.Medias;

namespace FMFT.Web.Server.Services.Coordinations.Medias
{
    public interface IMediaCoordinationService
    {
        ValueTask<Media> AddMediaFromFormFileAsync(IFormFile formFile);
        ValueTask<Media> RetrieveMediaByIdAsync(Guid mediaId);
    }
}
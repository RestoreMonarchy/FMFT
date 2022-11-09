using FMFT.Web.Server.Models.Medias;
using FMFT.Web.Server.Models.Medias.Params;

namespace FMFT.Web.Server.Services.Foundations.Medias
{
    public interface IMediaService
    {
        ValueTask<Media> AddMediaAsync(AddMediaParams @params);
        ValueTask<Media> AddMediaFromFormFileAsync(IFormFile formFile, int? userId);
        ValueTask<Media> RetrieveMediaByIdAsync(Guid mediaId);
    }
}
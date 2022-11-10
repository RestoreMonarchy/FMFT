using FMFT.Web.Client.Models.API;
using Microsoft.AspNetCore.Components.Forms;

namespace FMFT.Web.Client.Services.Medias
{
    public interface IMediaService
    {
        string GetMediaUrl(Guid mediaId);
        ValueTask<APIResponse> UploadBrowserFileAsync(IBrowserFile browserFile);
    }
}
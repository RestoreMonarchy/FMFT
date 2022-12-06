using FMFT.Web.Client.Models.API;
using Microsoft.AspNetCore.Components.Forms;

namespace FMFT.Web.Client.Services.Medias
{
    public interface IMediaService
    {
        string GetMediaUrl(Guid mediaId);
        string GetReservationTicketUrl(string reservationId, int reservationSeatId);
        ValueTask<APIResponse> UploadBrowserFileAsync(IBrowserFile browserFile);
    }
}
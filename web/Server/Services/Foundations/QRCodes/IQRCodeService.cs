using FMFT.Web.Server.Models.QRCodes;
using FMFT.Web.Server.Models.QRCodes.Params;

namespace FMFT.Web.Server.Services.Foundations.QRCodes
{
    public interface IQRCodeService
    {
        ValueTask<QRCodeImage> GenerateGuidQRCodeImageAsync(Guid guid);
        ValueTask<QRCodeImage> GenerateReservationTicketAsync(GenerateReservationTicketParams @params);
    }
}
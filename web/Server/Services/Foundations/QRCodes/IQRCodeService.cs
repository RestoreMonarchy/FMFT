using FMFT.Web.Server.Models.QRCodes;

namespace FMFT.Web.Server.Services.Foundations.QRCodes
{
    public interface IQRCodeService
    {
        ValueTask<QRCodeImage> GenerateReservationQRCodeImageAsync(string reservationId);
    }
}
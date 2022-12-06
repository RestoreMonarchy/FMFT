using FMFT.Features.Tickets.Models;
using FMFT.Web.Server.Models.QRCodes;

namespace FMFT.Web.Server.Brokers.QRCodes
{
    public interface IQRCodeBroker
    {
        ValueTask<QRCodeImage> GenerateGuidQRCodeImageAsync(Guid guid);
        ValueTask<QRCodeImage> GenerateReservationTicketAsync(ReservationTicketModel model);
    }
}

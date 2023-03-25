using FMFT.Features.Tickets.Models;
using FMFT.Web.Server.Brokers.QRCodes;
using FMFT.Web.Server.Models.QRCodes;
using FMFT.Web.Server.Models.QRCodes.Params;

namespace FMFT.Web.Server.Services.Foundations.QRCodes
{
    public class QRCodeService : IQRCodeService
    {
        private readonly IQRCodeBroker qrCodeBroker;

        public QRCodeService(IQRCodeBroker qrCodeBroker)
        {
            this.qrCodeBroker = qrCodeBroker;
        }

        public async ValueTask<QRCodeImage> GenerateGuidQRCodeImageAsync(Guid guid)
        {
            return await qrCodeBroker.GenerateGuidQRCodeImageAsync(guid);
        }

        public async ValueTask<QRCodeImage> GenerateReservationTicketAsync(GenerateReservationTicketParams @params)
        {
            ReservationTicketModel model = new()
            {
                SecretCode = @params.SecretCode,
                ShowName = @params.ShowName,
                ShowDate = @params.Date.DateTime,
                ReservationId = @params.ReservationId,
                SeatNumber = @params.Number,
                SeatRow = @params.Row,
                SeatSector = @params.Sector
            };

            return await qrCodeBroker.GenerateReservationTicketAsync(model);
        }
    }
}

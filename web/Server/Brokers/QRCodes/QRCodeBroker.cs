using FMFT.Features.Tickets.Models;
using FMFT.Features.Tickets.Services;
using FMFT.Web.Server.Models.QRCodes;
using FMFT.Web.Server.Models.QRCodes.Params;
using QRCoder;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;

namespace FMFT.Web.Server.Brokers.QRCodes
{
    public partial class QRCodeBroker : IQRCodeBroker
    {
        private readonly QRCodeGenerator generator;
        private readonly TicketGenerator ticketGenerator;

        public QRCodeBroker(TicketGenerator ticketGenerator)
        {
            generator = new();

            this.ticketGenerator = ticketGenerator;
        }

        public ValueTask<QRCodeImage> GenerateGuidQRCodeImageAsync(Guid guid)
        {
            string value = guid.ToString();

            Bitmap qrCodeBitmap = GetQRCodeBitmap(value);

            QRCodeImage image = new()
            {
                Data = GetBitmapToBytes(qrCodeBitmap, ImageFormat.Jpeg),
                ContentType = "image/jpeg"
            };

            return ValueTask.FromResult(image);            
        }

        public ValueTask<QRCodeImage> GenerateReservationTicketAsync(ReservationTicketModel model)
        {
            Bitmap ticketBitmap = ticketGenerator.GenerateReservationTicket(model);

            QRCodeImage image = new()
            {
                Data = GetBitmapToBytes(ticketBitmap, ImageFormat.Jpeg),
                ContentType = "image/jpeg"
            };

            return ValueTask.FromResult(image);
        }

        private Bitmap GetQRCodeBitmap(string value)
        {
            QRCodeData data = generator.CreateQrCode(value, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new(data);

            return qrCode.GetGraphic(20);
        }

        private byte[] GetBitmapToBytes(Bitmap bitmap, ImageFormat imageFormat)
        {
            using MemoryStream ms = new();
            bitmap.Save(ms, imageFormat);
            return ms.ToArray();
        }
    }
}

using FMFT.Web.Server.Models.QRCodes;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;

namespace FMFT.Web.Server.Brokers.QRCodes
{
    public partial class QRCodeBroker : IQRCodeBroker
    {
        private readonly QRCodeGenerator generator;

        public QRCodeBroker()
        {
            generator = new();
        }

        public Task<QRCodeImage> GenerateReservationQRCodeImageAsync(string reservationId)
        {
            QRCodeData data = generator.CreateQrCode(reservationId, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new(data);

            Bitmap bitmap = qrCode.GetGraphic(20);

            QRCodeImage image = new()
            {
                Data = GetBitmapToBytes(bitmap, ImageFormat.Jpeg),
                ContentType = "image/jpeg"
            };

            return Task.FromResult(image);            
        }

        private byte[] GetBitmapToBytes(Bitmap bitmap, ImageFormat imageFormat)
        {
            using MemoryStream ms = new();
            bitmap.Save(ms, imageFormat);
            return ms.ToArray();
        }
    }
}

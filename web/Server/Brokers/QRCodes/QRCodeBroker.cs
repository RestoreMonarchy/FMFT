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

        public QRCodeBroker()
        {
            generator = new();
        }

        public Task<QRCodeImage> GenerateGuidQRCodeImageAsync(Guid guid)
        {
            string value = guid.ToString();

            Bitmap qrCodeBitmap = GetQRCodeBitmap(value);

            QRCodeImage image = new()
            {
                Data = GetBitmapToBytes(qrCodeBitmap, ImageFormat.Jpeg),
                ContentType = "image/jpeg"
            };

            return Task.FromResult(image);            
        }

        public ValueTask<QRCodeImage> GenerateTicketAsync(GenerateTicketParams @params)
        {
            int height = 1600, width = 900;

            string qrCodeValue = @params.SecretCode.ToString();

            Bitmap backgroundBitmap = GetTicketBackgrund(width, height, Brushes.White);
            Bitmap qrCodeBitmap = GetQRCodeBitmap(qrCodeValue);

            using Graphics graphics = Graphics.FromImage(backgroundBitmap);

            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

            graphics.DrawImage(qrCodeBitmap, 0, 0);

            QRCodeImage image = new()
            {
                Data = GetBitmapToBytes(qrCodeBitmap, ImageFormat.Jpeg),
                ContentType = "image/jpeg"
            };

            return ValueTask.FromResult(image);
        }


        private Bitmap GetTicketBackgrund(int width, int height, Brush brush)
        {
            Rectangle rect = new(0, 0, width, height);
            Bitmap bitmap = new(rect.Width, rect.Height);
            bitmap.SetResolution(96, 96);

            using Graphics graphic = Graphics.FromImage(bitmap);

            graphic.FillRectangle(brush, rect);

            return bitmap;
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

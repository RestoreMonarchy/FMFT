using FMFT.Features.Tickets.Models;
using FMFT.Features.Tickets.Resources;
using QRCoder;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Globalization;

namespace FMFT.Features.Tickets.Services
{
    public class TicketGenerator
    {
        public Bitmap GenerateReservationTicket(ReservationTicketModel model)
        {
            // Load the ticket template from disk

            Bitmap template = Templates.TicketTemplate;

            // Create a new Bitmap with the desired size
            Bitmap ticket = new Bitmap(900, 1600);
            ticket.SetResolution(96, 96);

            using Graphics graphics = Graphics.FromImage(ticket);

            graphics.DrawImage(template, new Rectangle(0, 0, ticket.Width, ticket.Height));

            // Generate the QR code using the SecretCode property of the model
            QRCodeGenerator qrGenerator = new();
            QRCodeData qrData = qrGenerator.CreateQrCode(model.SecretCode.ToString(), QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new(qrData);

            Bitmap qrCodeBitmap = qrCode.GetGraphic(20);

            // Calculate the position of the QR code on the ticket
            int qrCodeX = (ticket.Width - qrCodeBitmap.Width) / 2;
            int qrCodeY = 230 + 82;

            // Draw the QR code on the ticket
            graphics.DrawImage(qrCodeBitmap, qrCodeX, qrCodeY);

            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

            Font font = new("Arial", 36);
                        
            // Calculate the position of the show name on the ticket
            int showNameX = (int)(ticket.Width - graphics.MeasureString(model.ShowName, font).Width) / 2;
            int showNameY = qrCodeY + qrCodeBitmap.Height + 82;

            // Draw the show name on the ticket
            graphics.DrawString(model.ShowName, font, Brushes.Black, showNameX, showNameY);

            Font font2 = new("Arial", 30);

            DateTime dateTime = TimeZoneInfo.ConvertTime(model.StartDate, TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time")).DateTime;
            CultureInfo culture = CultureInfo.GetCultureInfo("pl-PL");
            string dateString = dateTime.ToString("g", culture);

            int dateX = (int)(ticket.Width - graphics.MeasureString(dateString, font2).Width) / 2;
            int dateY = showNameY + 52 + 30;

            graphics.DrawString(dateString, font2, Brushes.Black, dateX, dateY);

            string seatString = $"Rząd: {model.SeatRow} Miejsce: {model.SeatNumber}";
            
            int seatX = (int)(ticket.Width - graphics.MeasureString(seatString, font2).Width) / 2;
            int seatY = dateY + 48 + 10;

            graphics.DrawString(seatString, font2, Brushes.Black, seatX, seatY);



            return ticket;
        }
    }
}

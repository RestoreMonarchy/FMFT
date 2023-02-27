using FMFT.Features.Tickets.Models;
using FMFT.Features.Tickets.Resources;
using QRCoder;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Globalization;
using System.Reflection;

namespace FMFT.Features.Tickets.Services
{
    public class TicketGenerator
    {
        public static readonly CultureInfo Culture = CultureInfo.GetCultureInfo("pl-PL");

        public Bitmap GenerateReservationTicket(ReservationTicketModel model)
        {
            Bitmap template = Templates.TicketTemplate;

            Bitmap ticket = new(900, 1600);

            ticket.SetResolution(96, 96);

            using Graphics graphics = Graphics.FromImage(ticket);
            
            // Set properties for the graphics
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

            // Draw background
            graphics.DrawImage(template, new Rectangle(0, 0, ticket.Width, ticket.Height));

            string secretCode = model.SecretCode.ToString();

            Bitmap qrCode = GenerateQRCodeFromString(secretCode);

            int positionY = 0;

            // Add margin of the gray header 
            positionY += 230;

            // Add margin from the bottom of header to QRCode position
            positionY += 82;

            // Calculate the horizontal position of the QR code on the ticket
            int qrCodeX = (ticket.Width - qrCode.Width) / 2;

            // Draw the QR code on the ticket
            graphics.DrawImage(qrCode, qrCodeX, positionY);

            positionY += qrCode.Height;

            Font bigFont = new("Arial", 36);
            Font smallFont = new("Arial", 30);
            Font tinyFont = new("Arial", 24);

            // Add a margin from the bottom of QRCode
            positionY += 82;

            // Calculate the position of the show name on the ticket
            string showName = model.ShowName;
            SizeF showNameSize = graphics.MeasureString(showName, bigFont);
            int showNameX = (ticket.Width - (int)showNameSize.Width) / 2;

            // Draw the show name on the ticket
            graphics.DrawString(showName, bigFont, Brushes.Black, showNameX, positionY);

            positionY += (int)showNameSize.Height;


            CultureInfo prevCulture = CultureInfo.CurrentCulture;
            CultureInfo.CurrentCulture = Culture;

            DateTime dateTime = model.ShowDate;
            string date = dateTime.ToLongDateString() + " " + dateTime.ToShortTimeString();
            //date = date.Substring(0, date.Length - 3);

            CultureInfo.CurrentCulture = prevCulture;

            SizeF dateSize = graphics.MeasureString(date, smallFont);
            int dateX = (ticket.Width - (int)dateSize.Width) / 2;
            positionY += 30;

            graphics.DrawString(date, smallFont, Brushes.Black, dateX, positionY);

            positionY += smallFont.Height;

            string seat = $"Rząd: {model.SeatRow} Miejsce: {model.SeatNumber}";
            SizeF seatSize = graphics.MeasureString(seat, smallFont);
            int seatX = (int)(ticket.Width - seatSize.Width) / 2;

            positionY += 20;

            graphics.DrawString(seat, smallFont, Brushes.Black, seatX, positionY);

            // Add footer
            int footerHeight = 150;

            string reservationId = $"ID rezerwacji: {model.ReservationId}";
            SizeF reservationIdSize = graphics.MeasureString(reservationId, tinyFont);

            int reservationIdMarginY = (footerHeight - (int)reservationIdSize.Height) / 2;
            int reservationIdMarginX = 50;

            int reservationIdX = (ticket.Width - (int)reservationIdSize.Width - reservationIdMarginX);
            int reservationIdY = ticket.Height  - footerHeight + reservationIdMarginY;

            graphics.DrawString(reservationId, tinyFont, Brushes.White, reservationIdX, reservationIdY);

            return ticket;
        }

        private Bitmap GenerateQRCodeFromString(string value)
        {
            QRCodeGenerator qrGenerator = new();
            QRCodeData qrData = qrGenerator.CreateQrCode(value, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new(qrData);

            return qrCode.GetGraphic(20);
        }
    }
}

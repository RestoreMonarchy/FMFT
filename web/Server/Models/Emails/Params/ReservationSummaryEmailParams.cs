using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace FMFT.Web.Server.Models.Emails.Params
{
    public class ReservationSummaryEmailParams
    {
        public string FirstName { get; set; }
        public string ShowName { get; set; }
        public string ReservationId { get; set; }
        public List<ReservationSeat> ReservationSeats { get; set; }
        public List<ReservationBulkItem> ReservationBulkItems { get; set; }

        public List<EmailAttachment> Attachments { get; set; }

        public class ReservationSeat
        {
            public int Row { get; set; }
            public int Number { get; set; }
            public char Sector { get; set; }
        }

        public class ReservationBulkItem
        {
            public int Id { get; set; }
            public string ProductName { get; set; }
        }
    }
}

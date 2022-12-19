namespace FMFT.Emails.Server.Models
{
    public class ReservationSummaryEmailModel
    {
        public string FirstName { get; set; }
        public string ShowName { get; set; }
        public string ReservationId { get; set; }
        public List<ReservationSeat> ReservationSeats { get; set; }

        public class ReservationSeat
        {
            public int Row { get; set; }
            public int Number { get; set; }
        }
    }
}

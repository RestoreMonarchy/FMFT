namespace FMFT.Features.Tickets.Models
{
    public class ReservationTicketModel
    {
        public Guid SecretCode { get; set; }
        public string ShowName { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public string ReservationId { get; set; }
        public int SeatRow { get; set; }
        public int SeatNumber { get; set; }
    }
}

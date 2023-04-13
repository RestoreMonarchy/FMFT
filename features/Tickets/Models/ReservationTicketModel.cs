namespace FMFT.Features.Tickets.Models
{
    public class ReservationTicketModel
    {
        public Guid SecretCode { get; set; }
        public string ShowName { get; set; }
        public DateTime ShowDate { get; set; }
        public string ReservationId { get; set; }
        public string SeatInformation { get; set; }
    }
}

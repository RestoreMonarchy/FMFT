namespace FMFT.Web.Shared.Models.Reservations
{
    public class Reservation
    {
        public int Id { get; set; }
        public int ShowId { get; set; }
        public int SeatId { get; set; }
        public int UserId { get; set; }
        public DateTime CreateDate { get; set; }
    }
}

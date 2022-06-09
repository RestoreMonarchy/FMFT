using FMFT.Web.Shared.Models.Seats;

namespace FMFT.Web.Shared.Models.Reservations
{
    public class Reservation
    {
        public int Id { get; set; }
        public int SeatId { get; set; }
        public DateTime CreateDate { get; set; }
    }
}

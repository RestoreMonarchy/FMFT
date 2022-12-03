using FMFT.Web.Server.Models.Seats;

namespace FMFT.Web.Server.Models.Reservations
{
    public class ReservationSeat
    {
        public int Id { get; set; }
        public Seat Seat { get; set; }
    }
}
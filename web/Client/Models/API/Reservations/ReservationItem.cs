using FMFT.Web.Client.Models.API.Seats;

namespace FMFT.Web.Client.Models.API.Reservations
{
    public class ReservationItem
    {
        public int Id { get; set; }
        public Seat Seat { get; set; }
        public bool IsScanned { get; set; }

        public bool IsBulk => Seat == null;
    }
}

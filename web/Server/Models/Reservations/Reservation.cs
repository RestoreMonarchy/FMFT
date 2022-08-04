using FMFT.Web.Server.Models.Seats;
using FMFT.Web.Server.Models.Shows;
using FMFT.Web.Server.Models.Users;

namespace FMFT.Web.Server.Models.Reservations
{
    public class Reservation
    {
        public int Id { get; set; }
        public ReservationStatus Status { get; set; }
        public DateTime CreateDate { get; set; }

        public Show Show { get; set; }
        public Seat Seat { get; set; }
        public UserInfo User { get; set; }
    }
}

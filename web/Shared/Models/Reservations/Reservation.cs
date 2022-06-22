using FMFT.Web.Shared.Models.Seats;
using FMFT.Web.Shared.Models.Shows;
using FMFT.Web.Shared.Models.Users;

namespace FMFT.Web.Shared.Models.Reservations
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

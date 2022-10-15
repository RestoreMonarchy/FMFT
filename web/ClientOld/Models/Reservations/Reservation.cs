using FMFT.Web.Client.Models.Seats;
using FMFT.Web.Client.Models.Shows;
using FMFT.Web.Client.Models.Users;
using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Client.Models.Reservations
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

using FMFT.Web.Client.Models.API.Seats;
using FMFT.Web.Client.Models.API.Shows;
using FMFT.Web.Client.Models.API.Users;
using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Client.Models.API.Reservations
{
    public class Reservation
    {
        public string Id { get; set; }
        public ReservationStatus Status { get; set; }
        public bool IsCanceled { get; set; }
        public DateTimeOffset CreateDate { get; set; }

        public Show Show { get; set; }
        public Seat Seat { get; set; }
        public UserInfo User { get; set; }
    }
}

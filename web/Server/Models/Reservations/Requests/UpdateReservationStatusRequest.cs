using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Server.Models.Reservations.Requests
{
    public class UpdateReservationStatusRequest
    {
        public int ReservationId { get; set; }
        public ReservationStatus Status { get; set; }
    }
}

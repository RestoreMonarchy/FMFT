using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Server.Models.Reservations.Requests
{
    public class UpdateReservationStatusRequest
    {
        public Guid ReservationId { get; set; }
        public ReservationStatus Status { get; set; }
    }
}

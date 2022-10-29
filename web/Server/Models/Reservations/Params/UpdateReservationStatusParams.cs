using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Server.Models.Reservations.Params
{
    public class UpdateReservationStatusParams
    {
        public Guid ReservationId { get; set; }
        public ReservationStatus ReservationStatus { get; set; }
        public DateTimeOffset UpdateStatusDate { get; set; }
        public int? AdminUserId { get; set; }
    }
}

using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Server.Models.Reservations.Params
{
    public class UpdateReservationStatusParams
    {
        public string ReservationId { get; set; }
        public ReservationStatus ReservationStatus { get; set; }
        public int? AdminUserId { get; set; }
    }
}

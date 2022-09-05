namespace FMFT.Web.Server.Models.Reservations.Params
{
    public class UpdateReservationStatusParams
    {
        public int ReservationId { get; set; }
        public ReservationStatus Status { get; set; }
        public DateTimeOffset UpdateStatusDate { get; set; }
        public int? AdminUserId { get; set; }        
    }
}

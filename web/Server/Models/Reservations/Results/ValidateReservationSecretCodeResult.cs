namespace FMFT.Web.Server.Models.Reservations.Results
{
    public class ValidateReservationSecretCodeResult
    {
        public Reservation Reservation { get; set; }
        public int? ReservationItemId { get; set; }
    }
}

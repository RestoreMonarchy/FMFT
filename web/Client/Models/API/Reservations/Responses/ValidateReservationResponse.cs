namespace FMFT.Web.Client.Models.API.Reservations.Responses
{
    public class ValidateReservationResponse
    {
        public Reservation Reservation { get; set; }
        public int? ReservationSeatId { get; set; }
    }
}

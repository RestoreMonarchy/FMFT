namespace FMFT.Web.Server.Models.Reservations.Exceptions
{
    public class NotFoundSeatReservationException : Exception
    {
        public NotFoundSeatReservationException()
            : base("ERR034: Reservation seat not found")
        {

        }
    }
}

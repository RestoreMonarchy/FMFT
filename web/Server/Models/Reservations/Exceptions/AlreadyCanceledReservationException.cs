namespace FMFT.Web.Server.Models.Reservations.Exceptions
{
    public class AlreadyCanceledReservationException : Exception
    {
        public AlreadyCanceledReservationException()
            : base("ERR036: Reservation is already canceled")
        {

        }
    }
}

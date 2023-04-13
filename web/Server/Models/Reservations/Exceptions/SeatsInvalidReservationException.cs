namespace FMFT.Web.Server.Models.Reservations.Exceptions
{
    public class SeatsInvalidReservationException : Exception
    {
        public SeatsInvalidReservationException() 
            : base("ERR059: One or more seat id are invalid in this reservation")
        {

        }
    }
}

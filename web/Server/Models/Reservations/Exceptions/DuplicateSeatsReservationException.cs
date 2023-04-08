namespace FMFT.Web.Server.Models.Reservations.Exceptions
{
    public class DuplicateSeatsReservationException : Exception
    {
        public DuplicateSeatsReservationException()
            : base("ERR058: One or more seats are duplicate in this reservation")
        {
            
        }
    }
}

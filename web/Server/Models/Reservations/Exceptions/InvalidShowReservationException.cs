namespace FMFT.Web.Server.Models.Reservations.Exceptions
{
    public class InvalidShowReservationException : Exception
    {
        public InvalidShowReservationException() 
            : base("ERR060: One or more seatIds are for a different show than specified")
        {
        }
    }
}

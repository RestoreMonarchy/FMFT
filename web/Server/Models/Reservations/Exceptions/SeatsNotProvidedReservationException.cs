namespace FMFT.Web.Server.Models.Reservations.Exceptions
{
    public class SeatsNotProvidedReservationException : Exception
    {
        public SeatsNotProvidedReservationException() 
            : base("ERR033: There are some non-bulk show products without SeatId")
        {

        }
    }
}

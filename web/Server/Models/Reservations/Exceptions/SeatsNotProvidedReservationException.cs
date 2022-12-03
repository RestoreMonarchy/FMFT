namespace FMFT.Web.Server.Models.Reservations.Exceptions
{
    public class SeatsNotProvidedReservationException : Exception
    {
        public SeatsNotProvidedReservationException() 
            : base("ERR033: The seats for the reservation were not provided")
        {

        }
    }
}

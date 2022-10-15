using Xeptions;

namespace FMFT.Web.Server.Models.Reservations.Exceptions
{
    public class NotFoundReservationException : Xeption
    {
        public NotFoundReservationException() 
            : base("ERR017: Reservation not found")
        {

        }
    }
}

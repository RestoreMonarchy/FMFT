using Xeptions;

namespace FMFT.Web.Server.Models.Reservations.Exceptions
{
    public class ReservationNotFoundException : Xeption
    {
        public ReservationNotFoundException() 
            : base("ERR017: Reservation not found")
        {

        }
    }
}

using Xeptions;

namespace FMFT.Web.Server.Models.Reservations.Exceptions
{
    public class SeatAlreadyReservedException : Xeption
    {
        public SeatAlreadyReservedException() 
            : base("This seat is already reserved in this show")
        {

        }
    }
}

using Xeptions;

namespace FMFT.Web.Server.Models.Reservations.Exceptions
{
    public class SeatAlreadyReservedException : Xeption
    {
        public SeatAlreadyReservedException() 
            : base("ERR018: Seat is already reserved")
        {

        }
    }
}

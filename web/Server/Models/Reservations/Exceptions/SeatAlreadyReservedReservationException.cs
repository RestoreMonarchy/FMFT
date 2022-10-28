using Xeptions;

namespace FMFT.Web.Server.Models.Reservations.Exceptions
{
    public class SeatAlreadyReservedReservationException : Exception
    {
        public SeatAlreadyReservedReservationException() 
            : base("ERR018: Seat is already reserved")
        {

        }
    }
}

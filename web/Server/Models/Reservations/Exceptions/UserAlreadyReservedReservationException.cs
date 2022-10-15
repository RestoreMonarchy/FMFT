using Xeptions;

namespace FMFT.Web.Server.Models.Reservations.Exceptions
{
    public class UserAlreadyReservedReservationException : Xeption
    {
        public UserAlreadyReservedReservationException() 
            : base("ERR019: User already created a reservation")
        {

        }
    }
}

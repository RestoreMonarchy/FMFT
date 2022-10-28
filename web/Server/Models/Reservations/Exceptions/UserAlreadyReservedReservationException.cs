using Xeptions;

namespace FMFT.Web.Server.Models.Reservations.Exceptions
{
    public class UserAlreadyReservedReservationException : Exception
    {
        public UserAlreadyReservedReservationException() 
            : base("ERR019: User already created a reservation")
        {

        }
    }
}

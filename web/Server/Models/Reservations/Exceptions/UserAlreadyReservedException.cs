using Xeptions;

namespace FMFT.Web.Server.Models.Reservations.Exceptions
{
    public class UserAlreadyReservedException : Xeption
    {
        public UserAlreadyReservedException() 
            : base("ERR019: User already created a reservation")
        {

        }
    }
}

using Xeptions;

namespace FMFT.Web.Server.Models.Reservations.Exceptions
{
    public class UserAlreadyReservedException : Xeption
    {
        public UserAlreadyReservedException() : base("This user has already reserved a seat for this show")
        {

        }
    }
}

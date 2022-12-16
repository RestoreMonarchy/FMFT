using Xeptions;

namespace FMFT.Web.Server.Models.Reservations.Exceptions
{
    public class CreateUserReservationValidationException : Xeption
    {
        public CreateUserReservationValidationException() 
            : base("ERR035: Create user reservation validation problem")
        {

        }
    }
}

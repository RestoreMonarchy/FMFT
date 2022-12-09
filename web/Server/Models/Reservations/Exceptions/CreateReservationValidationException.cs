using Xeptions;

namespace FMFT.Web.Server.Models.Reservations.Exceptions
{
    public class CreateReservationValidationException : Xeption
    {
        public CreateReservationValidationException() 
            : base("ERR035: Create reservation validation problem")
        {

        }
    }
}

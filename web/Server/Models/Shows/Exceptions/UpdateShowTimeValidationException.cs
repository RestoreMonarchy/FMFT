using Xeptions;

namespace FMFT.Web.Server.Models.Shows.Exceptions
{
    public class UpdateShowTimeValidationException : Xeption
    {
        public UpdateShowTimeValidationException() 
            : base("ERR056: Update Show Time validation problem")
        {

        }
    }
}

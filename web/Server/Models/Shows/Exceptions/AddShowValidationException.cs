using Xeptions;

namespace FMFT.Web.Server.Models.Shows.Exceptions
{
    public class AddShowValidationException : Xeption
    {
        public AddShowValidationException() 
            : base("ERR012: Add Show validation problem")
        {

        }
    }
}

using Xeptions;

namespace FMFT.Web.Server.Models.Shows.Exceptions
{
    public class UpdateShowValidationException : Xeption
    {
        public UpdateShowValidationException()
            : base("ERR013: Update Show validation problem")
        {

        }
    }
}

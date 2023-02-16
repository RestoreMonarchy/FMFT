using Xeptions;

namespace FMFT.Web.Server.Models.Shows.Exceptions
{
    public class UpdateShowStatusValidationException : Xeption
    {
        public UpdateShowStatusValidationException()
            : base("ERR055: Update Show Status validation problem")
        {

        }
    }
}

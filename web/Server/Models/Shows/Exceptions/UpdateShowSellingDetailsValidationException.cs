using Xeptions;

namespace FMFT.Web.Server.Models.Shows.Exceptions
{
    public class UpdateShowSellingDetailsValidationException : Xeption
    {
        public UpdateShowSellingDetailsValidationException()
            : base("ERR054: Update Show Selling Details validation problem")
        {

        }
    }
}

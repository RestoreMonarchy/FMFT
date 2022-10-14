using Xeptions;

namespace FMFT.Web.Server.Models.Shows.Exceptions
{
    public class NotFoundShowException : Xeption
    {
        public NotFoundShowException()
            : base("ERR014: Show not found")
        {

        }
    }
}

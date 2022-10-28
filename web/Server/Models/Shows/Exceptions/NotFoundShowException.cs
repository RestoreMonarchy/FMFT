using Xeptions;

namespace FMFT.Web.Server.Models.Shows.Exceptions
{
    public class NotFoundShowException : Exception
    {
        public NotFoundShowException()
            : base("ERR014: Show not found")
        {

        }
    }
}

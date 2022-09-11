using Xeptions;

namespace FMFT.Web.Server.Models.Shows.Exceptions
{
    public class ShowNotFoundException : Xeption
    {
        public ShowNotFoundException()
            : base("ERR014: Show not found")
        {

        }
    }
}

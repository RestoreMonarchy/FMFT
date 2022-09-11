using Xeptions;

namespace FMFT.Web.Server.Models.Shows.Exceptions
{
    public class ShowAuditoriumNotExistsException : Xeption
    {
        public ShowAuditoriumNotExistsException() 
            : base("ERR015: Show auditorium does not exist")
        {

        }
    }
}

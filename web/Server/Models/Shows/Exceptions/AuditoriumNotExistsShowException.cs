using Xeptions;

namespace FMFT.Web.Server.Models.Shows.Exceptions
{
    public class AuditoriumNotExistsShowException : Exception
    {
        public AuditoriumNotExistsShowException() 
            : base("ERR015: Show auditorium does not exist")
        {

        }
    }
}

using Xeptions;

namespace FMFT.Web.Server.Models.Seats.Exceptions
{
    public class NotFoundSeatException : Exception
    {
        public NotFoundSeatException() 
            : base("ERR016: Seat not found")
        {

        }
    }
}

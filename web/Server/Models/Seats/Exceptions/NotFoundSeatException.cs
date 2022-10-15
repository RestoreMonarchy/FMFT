using Xeptions;

namespace FMFT.Web.Server.Models.Seats.Exceptions
{
    public class NotFoundSeatException : Xeption
    {
        public NotFoundSeatException() 
            : base("ERR016: Seat not found")
        {

        }
    }
}

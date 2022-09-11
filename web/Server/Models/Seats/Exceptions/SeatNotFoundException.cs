using Xeptions;

namespace FMFT.Web.Server.Models.Seats.Exceptions
{
    public class SeatNotFoundException : Xeption
    {
        public SeatNotFoundException() 
            : base("ERR016: Seat not found")
        {

        }
    }
}

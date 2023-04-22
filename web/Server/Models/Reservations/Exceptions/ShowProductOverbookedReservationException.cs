namespace FMFT.Web.Server.Models.Reservations.Exceptions
{
    public class ShowProductOverbookedReservationException : Exception
    {
        public ShowProductOverbookedReservationException() 
            : base("ERR061: One or more bulk products are overbooked")
        {
        }
    }
}

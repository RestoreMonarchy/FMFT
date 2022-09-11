namespace FMFT.Web.Server.Models.Auditoriums.Exceptions
{
    public class AuditoriumNotFoundException : Exception
    {
        public AuditoriumNotFoundException() 
            : base("ERR020: Auditorium not found")
        {

        }
    }
}

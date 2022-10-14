namespace FMFT.Web.Server.Models.Auditoriums.Exceptions
{
    public class NotFoundAuditoriumException : Exception
    {
        public NotFoundAuditoriumException() 
            : base("ERR020: Auditorium not found")
        {

        }
    }
}

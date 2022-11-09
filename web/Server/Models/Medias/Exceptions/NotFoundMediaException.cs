namespace FMFT.Web.Server.Models.Medias.Exceptions
{
    public class NotFoundMediaException : Exception
    {
        public NotFoundMediaException()
            : base("ERR030: Media not found")
        {

        }
    }
}

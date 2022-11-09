namespace FMFT.Web.Server.Models.Medias.Exceptions
{
    public class NotSuppliedFileMediaException : Exception
    {
        public NotSuppliedFileMediaException()
            : base("ERR032: The media file is required")
        {

        }
    }
}

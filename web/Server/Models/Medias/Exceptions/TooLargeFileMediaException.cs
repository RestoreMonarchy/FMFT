namespace FMFT.Web.Server.Models.Medias.Exceptions
{
    public class TooLargeFileMediaException : Exception
    {
        public TooLargeFileMediaException()
            : base("ERR031: The media file is too large")
        {

        }
    }
}

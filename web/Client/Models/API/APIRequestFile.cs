namespace FMFT.Web.Client.Models.API
{
    public class APIRequestFile
    {
        public Stream FileStream { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
    }
}

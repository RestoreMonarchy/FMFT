namespace FMFT.Web.Server.Models.Options
{
    public class ServicesOptions
    {
        public const string SectionKey = "Services";

        public string APIBaseUrl { get; set; }
        public string ClientBaseUrl { get; set; }

        public string GetAPIUrl(string append)
        {
            append = "/" + append.TrimStart('/');

            return APIBaseUrl.TrimEnd('/') + append;
        }

        public string GetClientUrl(string append)
        {
            append = "/" + append.TrimStart('/');

            return ClientBaseUrl.TrimEnd('/') + append;
        }
    }
}

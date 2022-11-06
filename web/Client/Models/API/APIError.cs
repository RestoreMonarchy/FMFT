namespace FMFT.Web.Client.Models.API
{
    public class APIError
    {
        public string Title { get; set; }
        public Dictionary<string, string[]> Errors { get; set; }

        public Dictionary<string, string[]> Data => Errors;
        public string Code => Title.Substring(0, 6);
    }
}

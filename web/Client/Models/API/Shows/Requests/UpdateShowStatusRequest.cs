namespace FMFT.Web.Client.Models.API.Shows.Requests
{
    public class UpdateShowStatusRequest
    {
        public int ShowId { get; set; }
        public bool IsEnabled { get; set; }
    }
}

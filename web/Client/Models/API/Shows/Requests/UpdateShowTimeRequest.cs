namespace FMFT.Web.Client.Models.API.Shows.Requests
{
    public class UpdateShowTimeRequest
    {
        public int ShowId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
    }
}

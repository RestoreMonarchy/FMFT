namespace FMFT.Web.Client.Models.API.Shows.Requests
{
    public class UpdateShowSellingDetailsRequest
    {
        public int ShowId { get; set; }
        public DateTimeOffset SellStartDateTime { get; set; }
    }
}

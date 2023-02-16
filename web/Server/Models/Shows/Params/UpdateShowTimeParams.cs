namespace FMFT.Web.Server.Models.Shows.Params
{
    public class UpdateShowTimeParams
    {
        public int ShowId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
    }
}

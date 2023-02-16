namespace FMFT.Web.Server.Models.Shows.Params
{
    public class GetShowParams
    {
        public int? ShowId { get; set; }
        public bool Expired { get; set; } = true;
        public bool Disabled { get; set; } = true;
    }
}

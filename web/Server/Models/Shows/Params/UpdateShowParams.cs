namespace FMFT.Web.Server.Models.Shows.Params
{
    public class UpdateShowParams
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset StartDateTime { get; set; }
        public DateTimeOffset EndDateTime { get; set; }
        public int AuditoriumId { get; set; }
        public Guid? ThumbnailMediaId { get; set; }
        public DateTimeOffset SellStartDateTime { get; set; }
        public bool IsEnabled { get; set; }
    }
}

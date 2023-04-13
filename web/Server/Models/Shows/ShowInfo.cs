namespace FMFT.Web.Server.Models.Shows
{
    public class ShowInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset StartDateTime { get; set; }
        public DateTimeOffset EndDateTime { get; set; }
        public int AuditoriumId { get; set; }
        public Guid? ThumbnailMediaId { get; set; }
        public DateTimeOffset SellStartDateTime { get; set; }
        public bool IsEnabled { get; set; }
    }
}

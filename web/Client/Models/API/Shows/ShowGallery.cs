namespace FMFT.Web.Client.Models.API.Shows
{
    public class ShowGallery
    {
        public int Id { get; set; }
        public int ShowId { get; set; }
        public Guid MediaId { get; set; }
        public DateTimeOffset CreateDate { get; set; }
    }
}

namespace FMFT.Web.Server.Models.Shows.Params
{
    public class UpdateShowParams
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AuditoriumId { get; set; }
        public Guid? ThumbnailMediaId { get; set; }
    }
}

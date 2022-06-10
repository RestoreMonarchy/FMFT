namespace FMFT.Web.Shared.Models.Shows
{
    public class Show
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int AuditoriumId { get; set; }
    }
}

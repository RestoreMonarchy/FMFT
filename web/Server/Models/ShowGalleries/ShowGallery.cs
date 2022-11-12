using FMFT.Web.Server.Models.Users;

namespace FMFT.Web.Server.Models.ShowGalleries
{
    public class ShowGallery
    {
        public int Id { get; set; }
        public int ShowId { get; set; }
        public Guid MediaId { get; set; }
        public DateTimeOffset CreateDate { get; set; }
    }
}

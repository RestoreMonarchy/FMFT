using FMFT.Web.Client.Models.API.Users;

namespace FMFT.Web.Client.Models.API.Medias
{
    public class Media
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ContentType { get; set; }
        public DateTimeOffset CreateDate { get; set; }

        public UserInfo User { get; set; }
    }
}

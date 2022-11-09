using FMFT.Web.Server.Models.Users;
using System.Text.Json.Serialization;

namespace FMFT.Web.Server.Models.Medias
{
    public class Media
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public byte[] Content { get; set; }
        public string ContentType { get; set; }
        public DateTimeOffset CreateDate { get; set; }

        public UserInfo User { get; set; }
    }
}

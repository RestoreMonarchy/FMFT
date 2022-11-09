namespace FMFT.Web.Server.Models.Medias.DTOs
{
    public class InsertMediaDTO
    {
        public string Name { get; set; }
        public byte[] Content { get; set; }
        public string ContentType { get; set; }
        public int? UserId { get; set; }
    }
}

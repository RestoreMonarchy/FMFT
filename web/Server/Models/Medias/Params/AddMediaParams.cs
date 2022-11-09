namespace FMFT.Web.Server.Models.Medias.Params
{
    public class AddMediaParams
    {
        public string Name { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
        public int? UserId { get; set; }
    }
}

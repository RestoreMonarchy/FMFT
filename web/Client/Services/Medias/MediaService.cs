namespace FMFT.Web.Client.Services.Medias
{
    public class MediaService : IMediaService
    {
        private readonly IConfiguration configuration;

        private string APIUrl => configuration["APIUrl"];

        public MediaService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GetMediaUrl(Guid mediaId)
        {
            const string format = "{0}/media/file/{1}";

            return string.Format(format, APIUrl, mediaId);
        }
    }
}

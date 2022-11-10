using FMFT.Web.Client.Brokers.APIs;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Medias;
using Microsoft.AspNetCore.Components.Forms;

namespace FMFT.Web.Client.Services.Medias
{
    public class MediaService : IMediaService
    {
        private readonly IConfiguration configuration;
        private readonly IAPIBroker apiBroker;

        private string APIUrl => configuration["APIUrl"];

        private const long MaxAllowedSize = 5 * 1024 * 1024;

        public MediaService(IConfiguration configuration, IAPIBroker apiBroker)
        {
            this.configuration = configuration;
            this.apiBroker = apiBroker;
        }

        public string GetMediaUrl(Guid mediaId)
        {
            const string format = "{0}/media/file/{1}";

            return string.Format(format, APIUrl, mediaId);
        }        

        public async ValueTask<APIResponse> UploadBrowserFileAsync(IBrowserFile browserFile)
        {
            APIRequestFile requestFile = new()
            {
                FileName = browserFile.Name,
                ContentType = browserFile.ContentType,
                FileStream = browserFile.OpenReadStream(MaxAllowedSize)
            };

            return await apiBroker.UploadMediaAsync(requestFile);
        }
    }
}

using FMFT.Web.Client.Models.API;
using Microsoft.AspNetCore.Components.Forms;

namespace FMFT.Web.Client.Brokers.APIs
{
    public partial class APIBroker
    {
        public async ValueTask<APIResponse> GetAllMediaAsync()
        {
            const string url = "media";

            return await GetAsync(url);
        }

        public async ValueTask<APIResponse> UploadMediaAsync(IBrowserFile browserFile)
        {
            const string url = "media/upload";

            return await PostFileAsync(url, browserFile);
        }
    }
}
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Shows;
using FMFT.Web.Client.Models.API.Shows.Requests;

namespace FMFT.Web.Client.Brokers.APIs
{
    public partial class APIBroker
    {
        private const string ShowsRelativeUrl = "api/shows";

        public async ValueTask<APIResponse<Show>> GetShowByIdAsync(int showId)
        {
            return await GetAsync<Show>($"{ShowsRelativeUrl}/{showId}");
        }

        public async ValueTask<APIResponse<List<Show>>> GetAllShowsAsync()
        {
            return await GetAsync<List<Show>>(ShowsRelativeUrl);
        }

        public async ValueTask<APIResponse<Show>> AddShowAsync(AddShowRequest request)
        {
            return await PostAsync<Show>(ShowsRelativeUrl, request);
        }

        public async ValueTask<APIResponse<Show>> UpdateShowAsync(UpdateShowRequest request)
        {
            return await PutAsync<Show>(ShowsRelativeUrl, request);
        }
    }
}

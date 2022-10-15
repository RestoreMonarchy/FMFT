using FMFT.Web.Client.Models.API.Shows;
using FMFT.Web.Client.Models.API.Shows.Requests;
using FMFT.Web.Client.Models.Shows;
using FMFT.Web.Client.Models.Shows.Requests;

namespace FMFT.Web.Client.Brokers.APIs
{
    public partial class APIBroker
    {
        private const string ShowsRelativeUrl = "api/shows";

        public async ValueTask<Show> GetShowByIdAsync(int showId)
        {
            return await GetAsync<Show>($"{ShowsRelativeUrl}/{showId}");
        }

        public async ValueTask<List<Show>> GetAllShowsAsync()
        {
            return await GetAsync<List<Show>>(ShowsRelativeUrl);
        }

        public async ValueTask<Show> AddShowAsync(AddShowRequest request)
        {
            return await PostAsync<AddShowRequest, Show>(ShowsRelativeUrl, request);
        }

        public async ValueTask<Show> UpdateShowAsync(UpdateShowRequest request)
        {
            return await PutAsync<UpdateShowRequest, Show>(ShowsRelativeUrl, request);
        }
    }
}

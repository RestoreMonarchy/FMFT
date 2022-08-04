using FMFT.Web.Server.Models.Shows;

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
    }
}

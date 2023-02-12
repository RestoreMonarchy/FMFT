using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Auditoriums;

namespace FMFT.Web.Client.Brokers.APIs
{
    public partial class APIBroker
    {
        private const string AuditoriumsRelativeUrl = "api/auditoriums";

        public async ValueTask<APIResponse<Auditorium>> GetAuditoriumByIdAsync(int auditoriumId)
        {
            return await GetAsync<Auditorium>($"{AuditoriumsRelativeUrl}/{auditoriumId}");
        }
        public async ValueTask<APIResponse<Auditorium>> GetAuditoriumByShowIdAsync(int showId)
        {
            return await GetAsync<Auditorium>($"{AuditoriumsRelativeUrl}/showid/{showId}");
        }
        public async ValueTask<APIResponse<List<Auditorium>>> GetAllAuditoriumsAsync()
        {
            return await GetAsync<List<Auditorium>>(AuditoriumsRelativeUrl);
        }
    }
}

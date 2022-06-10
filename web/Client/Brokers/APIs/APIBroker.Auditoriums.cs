using FMFT.Web.Shared.Models.Auditoriums;

namespace FMFT.Web.Client.Brokers.APIs
{
    public partial class APIBroker
    {
        private const string AuditoriumsRelativeUrl = "api/auditoriums";

        public async ValueTask<Auditorium> GetAuditoriumByIdAsync(int auditoriumId)
        {
            return await GetAsync<Auditorium>($"{AuditoriumsRelativeUrl}/{auditoriumId}");
        }

        public async ValueTask<List<Auditorium>> GetAllAuditoriumsAsync()
        {
            return await GetAsync<List<Auditorium>>(AuditoriumsRelativeUrl);
        }
    }
}

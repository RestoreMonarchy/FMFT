using FMFT.Web.Client.Models.API.Shows;
using FMFT.Web.Client.Models.API.Shows.Requests;

namespace FMFT.Web.Client.Brokers.APIs
{
    public partial interface IAPIBroker
    {
        ValueTask<Show> GetShowByIdAsync(int showId);
        ValueTask<List<Show>> GetAllShowsAsync();
        ValueTask<Show> AddShowAsync(AddShowRequest request);
        ValueTask<Show> UpdateShowAsync(UpdateShowRequest request);
    }
}

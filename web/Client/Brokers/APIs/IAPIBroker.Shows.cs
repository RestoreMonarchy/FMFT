using FMFT.Web.Client.Models.Shows;
using FMFT.Web.Client.Models.Shows.Requests;

namespace FMFT.Web.Client.Brokers.APIs
{
    public partial interface IAPIBroker
    {
        ValueTask<Show> GetShowByIdAsync(int showId);
        ValueTask<List<Show>> GetAllShowsAsync();
        ValueTask<Show> PostShowAsync(PostShowRequest request);
        ValueTask<Show> PutShowAsync(PutShowRequest request);
    }
}

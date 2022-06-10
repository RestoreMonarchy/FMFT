using FMFT.Web.Shared.Models.Shows;

namespace FMFT.Web.Client.Brokers.APIs
{
    public partial interface IAPIBroker
    {
        ValueTask<Show> GetShowByIdAsync(int showId);
        ValueTask<List<Show>> GetAllShowsAsync();
    }
}

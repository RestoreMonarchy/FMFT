using FMFT.Web.Shared.Models.Shows;

namespace FMFT.Web.Server.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Show> SelectShowByIdAsync(int showId);
        ValueTask<IEnumerable<Show>> SelectAllShowsAsync();
    }
}

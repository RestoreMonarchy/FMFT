using FMFT.Web.Shared.Models.Shows;

namespace FMFT.Web.Server.Services.Foundations.Shows
{
    public interface IShowService
    {
        ValueTask<IEnumerable<Show>> RetrieveAllShowsAsync();
        ValueTask<Show> RetrieveShowByIdAsync(int showId);
    }
}

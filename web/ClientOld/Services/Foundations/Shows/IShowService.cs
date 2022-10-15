using FMFT.Web.Client.Models.Shows;
using FMFT.Web.Client.Models.Shows.Params;

namespace FMFT.Web.Client.Services.Foundations.Shows
{
    public interface IShowService
    {
        ValueTask<Show> AddShowAsync(AddShowParams @params);
        ValueTask<List<Show>> RetrieveAllShowsAsync();
        ValueTask<Show> RetrieveShowByIdAsync(int showId);
        ValueTask<Show> UpdateShowAsync(UpdateShowParams @params);
    }
}

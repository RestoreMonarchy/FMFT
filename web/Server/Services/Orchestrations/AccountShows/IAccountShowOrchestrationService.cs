using FMFT.Web.Server.Models.Shows;
using FMFT.Web.Server.Models.Shows.Params;

namespace FMFT.Web.Server.Services.Orchestrations.AccountShows
{
    public interface IAccountShowOrchestrationService
    {
        ValueTask<Show> AddShowAsync(AddShowParams @params);
        ValueTask<Show> ModifyShowAsync(UpdateShowParams @params);
        ValueTask<IEnumerable<Show>> RetrieveAllShowsAsync();
        ValueTask<Show> RetrieveShowByIdAsync(int showId);
    }
}
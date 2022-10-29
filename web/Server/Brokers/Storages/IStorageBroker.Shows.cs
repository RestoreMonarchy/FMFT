using FMFT.Web.Server.Models.Database;
using FMFT.Web.Server.Models.Shows;
using FMFT.Web.Server.Models.Shows.Params;

namespace FMFT.Web.Server.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Show> SelectShowByIdAsync(int showId);
        ValueTask<IEnumerable<Show>> SelectAllShowsAsync();
        ValueTask<StoredProcedureResult<Show>> ExecuteAddShowAsync(AddShowParams @params);
        ValueTask<StoredProcedureResult<Show>> ExecuteUpdateShowAsync(UpdateShowParams @params);
    }
}

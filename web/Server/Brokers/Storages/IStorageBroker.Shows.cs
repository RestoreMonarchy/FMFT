using FMFT.Web.Server.Models.Database;
using FMFT.Web.Server.Models.Shows;
using FMFT.Web.Server.Models.Shows.Params;

namespace FMFT.Web.Server.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Show> SelectShowByIdAsync(int showId);
        ValueTask<IEnumerable<Show>> SelectAllShowsAsync();
        ValueTask<IEnumerable<Show>> SelectPublicShowsAsync();
        ValueTask<Show> SelectPublicShowByIdAsync(int showId);
        ValueTask<StoredProcedureResult<Show>> ExecuteAddShowAsync(AddShowParams @params);
        ValueTask<StoredProcedureResult<Show>> ExecuteUpdateShowAsync(UpdateShowParams @params);
        ValueTask<StoredProcedureResult<Show>> ExecuteUpdateShowSellingDetailsAsync(UpdateShowSellingDetailsParams @params);
        ValueTask<StoredProcedureResult<Show>> ExecuteUpdateShowStatusAsync(UpdateShowStatusParams @params);
    }
}

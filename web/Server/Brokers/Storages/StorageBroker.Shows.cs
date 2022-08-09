using Dapper;
using FMFT.Web.Server.Models.Database;
using FMFT.Web.Server.Models.Shows;
using FMFT.Web.Server.Models.Shows.Params;

namespace FMFT.Web.Server.Brokers.Storages
{
    public partial class StorageBroker
    {
        public async ValueTask<Show> SelectShowByIdAsync(int showId)
        {
            const string sql = "SELECT * FROM dbo.Shows WHERE Id = @showId;";
            return await connection.QuerySingleOrDefaultAsync<Show>(sql, new { showId });
        }

        public async ValueTask<IEnumerable<Show>> SelectAllShowsAsync()
        {
            const string sql = "SELECT * FROM dbo.Shows;";
            return await connection.QueryAsync<Show>(sql);
        }

        public async ValueTask<StoredProcedureResult<Show>> ExecuteAddShowAsync(AddShowParams @params)
        {
            const string sql = "dbo.AddShow";
            return await QueryStoredProcedureSingleResultAsync<Show>(sql, @params);
        }

        public async ValueTask<StoredProcedureResult<Show>> ExecuteUpdateShowAsync(UpdateShowParams @params)
        {
            const string sql = "dbo.UpdateShow";
            return await QueryStoredProcedureSingleResultAsync<Show>(sql, @params);
        }
    }
}

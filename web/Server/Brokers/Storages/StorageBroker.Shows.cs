using Dapper;
using FMFT.Web.Server.Models.Shows;

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
    }
}

using Dapper;
using FMFT.Web.Server.Models.Seats;

namespace FMFT.Web.Server.Brokers.Storages
{
    public partial class StorageBroker
    {
        public async ValueTask<Seat> SelectSeatByIdAsync(int seatId)
        {
            const string sql = "SELECT * FROM dbo.Seats WHERE Id = @seatId;";
            return await connection.QuerySingleOrDefaultAsync<Seat>(sql, new { seatId });
        }

        public async ValueTask<IEnumerable<Seat>> SelectAllSeatsAsync()
        {
            const string sql = "SELECT * FROM dbo.Seats;";
            return await connection.QueryAsync<Seat>(sql);
        }
    }
}

using Dapper;
using FMFT.Web.Server.Models.Database;
using FMFT.Web.Shared.Models.Reservations;
using FMFT.Web.Shared.Models.Reservations.Params;

namespace FMFT.Web.Server.Brokers.Storages
{
    public partial class StorageBroker
    {
        public async ValueTask<Reservation> GetReservationAsync(int reservationId)
        {
            const string sql = "SELECT * FROM dbo.Reservations WHERE Id = @reservationId;";
            return await connection.QuerySingleOrDefaultAsync<Reservation>(sql, new { reservationId });
        }

        public async ValueTask<StoredProcedureResult<Reservation>> CreateReservationAsync(
            CreateReservationParams @params)
        {
            const string sql = "dbo.CreateReservation";
            return await QueryStoredProcedureSingleResultAsync<Reservation>(sql, @params);
        }
    }
}

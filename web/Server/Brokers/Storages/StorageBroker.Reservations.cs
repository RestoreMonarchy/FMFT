using Dapper;
using FMFT.Web.Server.Models.Database;
using FMFT.Web.Server.Models.Reservations;
using FMFT.Web.Server.Models.Reservations.Params;
using FMFT.Web.Server.Models.Seats;
using FMFT.Web.Server.Models.Shows;
using FMFT.Web.Server.Models.Users;
using System.Data;

namespace FMFT.Web.Server.Brokers.Storages
{
    public partial class StorageBroker
    {
        public async ValueTask<Reservation> SelectReservationByIdAsync(int reservationId)
        {
            const string sql = @"SELECT r.*, s.*, se.*, u.* FROM dbo.Reservations r
                JOIN dbo.Shows s ON s.Id = r.ShowId
                JOIN dbo.Seats se ON se.Id = r.SeatId
                JOIN dbo.Users u ON u.Id = r.UserId
                WHERE r.Id = @reservationId;";

            return await QueryReservationAsync(sql, new { reservationId });
        }

        public async ValueTask<IEnumerable<Reservation>> SelectAllReservationsAsync()
        {
            const string sql = @"SELECT r.*, s.*, se.*, u.* FROM dbo.Reservations r
                JOIN dbo.Shows s ON s.Id = r.ShowId
                JOIN dbo.Seats se ON se.Id = r.SeatId
                JOIN dbo.Users u ON u.Id = r.UserId;";

            return await QueryReservationsAsync(sql);
        }

        public async ValueTask<IEnumerable<Reservation>> SelectReservationsByUserIdAsync(int userId)
        {
            const string sql = @"SELECT r.*, s.*, se.*, u.* FROM dbo.Reservations r
                JOIN dbo.Shows s ON s.Id = r.ShowId
                JOIN dbo.Seats se ON se.Id = r.SeatId
                JOIN dbo.Users u ON u.Id = r.UserId
                WHERE r.UserId = @userId;";

            return await QueryReservationsAsync(sql, new { userId });
        }

        public async ValueTask<StoredProcedureResult<Reservation>> CreateReservationAsync(
            CreateReservationParams @params)
        {
            const string sql = "dbo.CreateReservation";
            StoredProcedureResult<Reservation> result = new();
            DynamicParameters parameters = StoredProcedureParameters(@params);

            result.Result = await QueryReservationAsync(sql, parameters, CommandType.StoredProcedure);
            result.ReturnValue = GetReturnValue(parameters);
            return result;
        }

        private async ValueTask<Reservation> QueryReservationAsync(string sql, object param = null, CommandType? commandType = null)
        {
            Reservation reservation = null;

            await connection.QueryAsync<Reservation, Show, Seat, UserInfo, Reservation>(sql, (r, s, se, u) => 
            {
                reservation = r;
                reservation.Show = s;
                reservation.Seat = se;
                reservation.User = u;
                return null;
            }, param, commandType: commandType);

            return reservation;
        }

        private async ValueTask<IEnumerable<Reservation>> QueryReservationsAsync(string sql, object param = null)
        {
            return await connection.QueryAsync<Reservation, Show, Seat, UserInfo, Reservation>(sql, (r, s, se, u) =>
            {
                r.Show = s;
                r.Seat = se;
                r.User = u;
                return r;
            }, param);
        }
    }
}

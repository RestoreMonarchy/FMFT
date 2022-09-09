﻿using Dapper;
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
            GetReservationParams @params = new()
            {
                ReservationId = reservationId
            };
            return await GetReservationAsync(@params);
        }

        public async ValueTask<IEnumerable<Reservation>> SelectAllReservationsAsync()
        {
            GetReservationParams @params = new();
            return await GetReservationsAsync(@params);
        }

        public async ValueTask<IEnumerable<Reservation>> SelectReservationsByUserIdAsync(int userId)
        {
            GetReservationParams @params = new()
            {
                UserId = userId
            };
            return await GetReservationsAsync(@params);
        }

        public async ValueTask<IEnumerable<Reservation>> SelectReservationsByShowIdAsync(int showId)
        {
            GetReservationParams @params = new()
            {
                ShowId = showId
            };
            return await GetReservationsAsync(@params);
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

        public async ValueTask<StoredProcedureResult<Reservation>> UpdateReservationStatusAsync(UpdateReservationStatusParams @params)
        {
            const string sql = "dbo.UpdateReservationStatus";
            StoredProcedureResult<Reservation> result = new();
            DynamicParameters parameters = StoredProcedureParameters(@params);

            result.Result = await QueryReservationAsync(sql, parameters, CommandType.StoredProcedure);
            result.ReturnValue = GetReturnValue(parameters);
            return result;
        }
        
        public async ValueTask<Reservation> GetReservationAsync(GetReservationParams @params)
        {
            IEnumerable<Reservation> reservations = await GetReservationsAsync(@params);
            return reservations.FirstOrDefault();
        }

        private async ValueTask<IEnumerable<Reservation>> GetReservationsAsync(GetReservationParams @params)
        {
            const string sql = "dbo.GetReservations";
            return await QueryReservationsAsync(sql, @params, CommandType.StoredProcedure);
        }

        private async ValueTask<Reservation> QueryReservationAsync(string sql, object param = null, CommandType? commandType = null)
        {
            Reservation reservation = null;

            await connection.QueryAsync<Reservation, Show, Seat, UserInfo, UserInfo, Reservation>(sql, (r, s, se, u, au) => 
            {
                reservation = r;
                reservation.Show = s;
                reservation.Seat = se;
                reservation.User = u;
                reservation.AdminUser = au;
                return null;
            }, param, commandType: commandType);

            return reservation;
        }

        private async ValueTask<IEnumerable<Reservation>> QueryReservationsAsync(string sql, object param = null, CommandType? commandType = null)
        {
            return await connection.QueryAsync<Reservation, Show, Seat, UserInfo, UserInfo, Reservation>(sql, (r, s, se, u, au) =>
            {
                r.Show = s;
                r.Seat = se;
                r.User = u;
                r.AdminUser = au;
                return r;
            }, param, commandType: commandType);
        }
    }
}

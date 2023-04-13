using Dapper;
using FMFT.Web.Server.Models.Database;
using FMFT.Web.Server.Models.Reservations;
using FMFT.Web.Server.Models.Reservations.DTOs;
using FMFT.Web.Server.Models.Reservations.Params;
using FMFT.Web.Server.Models.Reservations.Results;
using FMFT.Web.Server.Models.Seats;
using FMFT.Web.Server.Models.ShowProducts;
using FMFT.Web.Server.Models.Shows;
using FMFT.Web.Server.Models.Users;
using Microsoft.Extensions.Logging.Abstractions;
using System.Data;
using System.Security.AccessControl;

namespace FMFT.Web.Server.Brokers.Storages
{
    public partial class StorageBroker
    {
        public async ValueTask<Reservation> SelectReservationByIdAsync(string reservationId)
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

        public async ValueTask<IEnumerable<Reservation>> SelectReservationsByOrderIdAsync(int orderId)
        {
            GetReservationParams @params = new()
            {
                OrderId = orderId
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

        public async ValueTask<IEnumerable<Reservation>> SelectReservationsByUserAndShowIdAsync(int userId, int showId)
        {
            GetReservationParams @params = new()
            {
                UserId = userId,
                ShowId = showId
            };

            return await GetReservationsAsync(@params);
        }

        public async ValueTask<StoredProcedureResult<Reservation>> CreateReservationAsync(
            CreateReservationDTO dto)
        {
            const string sql = "dbo.CreateReservation";
            StoredProcedureResult<Reservation> result = new();
            DynamicParameters parameters = StoredProcedureParameters(dto);

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
        
        public async ValueTask<StoredProcedureResult<Reservation>> CancelReservationAsync(string reservationId)
        {
            const string sql = "dbo.CancelReservation";
            StoredProcedureResult<Reservation> result = new();
            DynamicParameters parameters = StoredProcedureParameters(new 
            { 
                ReservationId = reservationId
            });

            result.Result = await QueryReservationAsync(sql, parameters, CommandType.StoredProcedure);
            result.ReturnValue = GetReturnValue(parameters);

            return result;
        }

        public async ValueTask<StoredProcedureResult<ValidateReservationSecretCodeResult>> ValidateReservationSecretCodeAsync(Guid secretCode)
        {
            const string sql = "dbo.GetReservationBySecretKey";
            StoredProcedureResult<ValidateReservationSecretCodeResult> spResult = new();
            DynamicParameters parameters = StoredProcedureParameters(new 
            { 
                SecretCode = secretCode
            });
            parameters.Add("ReservationItemId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("ScanDate", dbType: DbType.DateTime, direction: ParameterDirection.Output);

            ValidateReservationSecretCodeResult result = new();

            result.Reservation = await QueryReservationAsync(sql, parameters, CommandType.StoredProcedure);
            result.ReservationItemId = parameters.Get<int?>("ReservationItemId");
            DateTime? scanDate = parameters.Get<DateTime?>("ScanDate");
            result.ScanDate = scanDate.HasValue ? scanDate.Value : null;

            spResult.Result = result;
            spResult.ReturnValue = GetReturnValue(parameters);

            return spResult;
        }

        private async ValueTask<Reservation> GetReservationAsync(GetReservationParams @params)
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

            Type[] types = new Type[]
            {
                typeof(Reservation),
                typeof(ShowInfo),
                typeof(UserInfo),
                typeof(ReservationDetails),
                typeof(ReservationItem),
                typeof(ShowProduct),
                typeof(Seat)
            };

            await connection.QueryAsync<Reservation>(sql, types, objects => 
            {
                int i = 0;
                Reservation r = objects[i++] as Reservation;
                ShowInfo s = objects[i++] as ShowInfo;
                UserInfo u = objects[i++] as UserInfo;
                ReservationDetails rd = objects[i++] as ReservationDetails;
                ReservationItem ri = objects[i++] as ReservationItem;
                ShowProduct sp = objects[i++] as ShowProduct;
                Seat se = objects[i++] as Seat;

                if (reservation == null)
                {
                    reservation = r;
                    reservation.Show = s;
                    reservation.User = u;

                    if (rd != null && rd.ReservationId != null)
                    {
                        reservation.Details = rd;
                    }

                    reservation.Items = new();
                }

                if (ri != null)
                {
                    ri.ShowProduct = sp;
                    ri.Seat = se;                    
                    reservation.Items.Add(ri);
                }                

                return null;
            }, param, commandType: commandType);

            return reservation;
        }

        private async ValueTask<IEnumerable<Reservation>> QueryReservationsAsync(string sql, object param = null, CommandType? commandType = null)
        {
            List<Reservation> reservations = new();

            Type[] types = new Type[]
            {
                typeof(Reservation),
                typeof(ShowInfo),
                typeof(UserInfo),
                typeof(ReservationDetails),
                typeof(ReservationItem),
                typeof(ShowProduct),
                typeof(Seat)
            };

            await connection.QueryAsync<Reservation>(sql, types, objects =>
            {
                int i = 0;
                Reservation r = objects[i++] as Reservation;
                ShowInfo s = objects[i++] as ShowInfo;
                UserInfo u = objects[i++] as UserInfo;
                ReservationDetails rd = objects[i++] as ReservationDetails;
                ReservationItem ri = objects[i++] as ReservationItem;
                ShowProduct sp = objects[i++] as ShowProduct;
                Seat se = objects[i++] as Seat;

                Reservation reservation = reservations.FirstOrDefault(x => x.Id == r.Id);

                if (reservation == null)
                {
                    reservation = r;
                    reservation.Show = s;
                    reservation.User = u;

                    if (rd != null && rd.ReservationId != null)
                    {
                        reservation.Details = rd;
                    }
                    
                    reservation.Items = new();

                    reservations.Add(reservation);
                }

                if (ri != null)
                {
                    ri.ShowProduct = sp;
                    ri.Seat = se;
                    reservation.Items.Add(ri);
                }

                return null;
            }, param, commandType: commandType);

            return reservations;
        }
    }
}

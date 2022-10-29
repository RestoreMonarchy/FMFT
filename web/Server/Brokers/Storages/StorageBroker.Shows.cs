using Dapper;
using FMFT.Web.Server.Models.Database;
using FMFT.Web.Server.Models.Shows;
using FMFT.Web.Server.Models.Shows.Params;
using System.Data;

namespace FMFT.Web.Server.Brokers.Storages
{
    public partial class StorageBroker
    {
        public async ValueTask<Show> SelectShowByIdAsync(int showId)
        {
            GetShowParams @params = new()
            {
                ShowId = showId
            };

            return await QueryShowAsync(@params);
        }

        public async ValueTask<IEnumerable<Show>> SelectAllShowsAsync()
        {
            GetShowParams @params = new();

            return await QueryShowsAsync(@params);
        }

        private async ValueTask<Show> QueryShowAsync(GetShowParams @params)
        {
            const string sql = "dbo.GetShows";

            Show show = null;

            await connection.QueryAsync<Show, ShowReservedSeat, Show>(sql, (s, rs) =>
            {
                if (show == null)
                {
                    show = s;
                    show.ReservedSeats = new();
                }

                if (rs != null)
                {
                    show.ReservedSeats.Add(rs);
                }

                return null;
            }, @params, commandType: CommandType.StoredProcedure, splitOn: "SeatId");

            return show;
        }

        private async ValueTask<IEnumerable<Show>> QueryShowsAsync(GetShowParams @params)
        {
            const string sql = "dbo.GetShows";

            List<Show> shows = new();

            await connection.QueryAsync<Show, ShowReservedSeat, Show>(sql, (s, rs) =>
            {
                Show show = shows.FirstOrDefault(x => x.Id == s.Id);

                if (show == null)
                {
                    show = s;
                    show.ReservedSeats = new();
                    shows.Add(show);
                }

                if (rs != null)
                {
                    show.ReservedSeats.Add(rs);
                }

                return null;
            }, @params, commandType: CommandType.StoredProcedure, splitOn: "SeatId");

            return shows;
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

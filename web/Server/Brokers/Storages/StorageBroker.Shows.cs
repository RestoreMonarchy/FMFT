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

        public async ValueTask<Show> SelectPublicShowByIdAsync(int showId)
        {
            GetShowParams @params = new()
            {
                ShowId = showId,
                Disabled = false
            };

            return await QueryShowAsync(@params);
        }

        public async ValueTask<IEnumerable<Show>> SelectPublicShowsAsync()
        {
            GetShowParams @params = new()
            {
                Disabled = false,
                Expired = true
            };

            return await QueryShowsAsync(@params);
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

            await connection.QueryAsync<Show, ShowReservedItem, Show>(sql, (s, rs) =>
            {
                if (show == null)
                {
                    show = s;
                    show.ReservedItems = new();
                }

                if (rs != null)
                {
                    show.ReservedItems.Add(rs);
                }

                return null;
            }, @params, commandType: CommandType.StoredProcedure);

            return show;
        }

        private async ValueTask<IEnumerable<Show>> QueryShowsAsync(GetShowParams @params)
        {
            const string sql = "dbo.GetShows";

            List<Show> shows = new();

            await connection.QueryAsync<Show, ShowReservedItem, Show>(sql, (s, ri) =>
            {
                Show show = shows.FirstOrDefault(x => x.Id == s.Id);

                if (show == null)
                {
                    show = s;
                    show.ReservedItems = new();
                    shows.Add(show);
                }

                if (ri != null)
                {
                    show.ReservedItems.Add(ri);
                }

                return null;
            }, @params, commandType: CommandType.StoredProcedure);

            return shows;
        }

        private async ValueTask<StoredProcedureResult<Show>> QueryShowStoredProcedureAsync(string procedureName, dynamic parameters)
        {
            Show show = null;

            DynamicParameters p = StoredProcedureParameters(parameters);

            await connection.QueryAsync<Show, ShowReservedItem, Show>(procedureName, (s, ri) =>
            {
                if (show == null)
                {
                    show = s;
                    show.ReservedItems = new();
                }

                if (ri != null)
                {
                    show.ReservedItems.Add(ri);
                }

                return null;
            }, p, commandType: CommandType.StoredProcedure);

            StoredProcedureResult<Show> result = new();
            result.Result = show;
            result.ReturnValue = GetReturnValue(p);

            return result;
        }

        public async ValueTask<StoredProcedureResult<Show>> ExecuteAddShowAsync(AddShowParams @params)
        {
            const string sql = "dbo.AddShow";

            return await QueryShowStoredProcedureAsync(sql, @params);
        }

        public async ValueTask<StoredProcedureResult<Show>> ExecuteUpdateShowAsync(UpdateShowParams @params)
        {
            const string sql = "dbo.UpdateShow";

            return await QueryShowStoredProcedureAsync(sql, @params);
        }

        public async ValueTask<StoredProcedureResult<Show>> ExecuteUpdateShowSellingDetailsAsync(UpdateShowSellingDetailsParams @params)
        {
            const string sql = "dbo.UpdateShowSellingDetails";

            return await QueryShowStoredProcedureAsync(sql, @params);
        }

        public async ValueTask<StoredProcedureResult<Show>> ExecuteUpdateShowStatusAsync(UpdateShowStatusParams @params)
        {
            const string sql = "dbo.UpdateShowStatus";

            return await QueryShowStoredProcedureAsync(sql, @params);
        }

        public async ValueTask<StoredProcedureResult<Show>> ExecuteUpdateShowTimeAsync(UpdateShowTimeParams @params)
        {
            const string sql = "dbo.UpdateShowTime";

            return await QueryShowStoredProcedureAsync(sql, @params);
        }
    }
}

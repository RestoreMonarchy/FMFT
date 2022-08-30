using Dapper;
using FMFT.Web.Server.Models.Database;
using System.Data;
using System.Data.SqlClient;

namespace FMFT.Web.Server.Brokers.Storages
{
    public partial class StorageBroker : IStorageBroker
    {
        private readonly IConfiguration configuration;
        private readonly SqlConnection connection;

        public StorageBroker(IConfiguration configuration)
        {
            this.configuration = configuration;
            connection = new SqlConnection(configuration.GetConnectionString("Default"));
        }

        private DynamicParameters StoredProcedureParameters(dynamic parameters)
        {
            DynamicParameters p = new();
            p.AddDynamicParams(parameters);
            p.Add(name: "@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
            return p;
        }

        private int GetReturnValue(DynamicParameters p)
        {
            return p.Get<int>("@ReturnValue");
        }

        private async ValueTask<StoredProcedureResult> ExecuteStoredProcedureAsync(
            string storedProcedureName, 
            dynamic parameters)
        {
            DynamicParameters p = StoredProcedureParameters(parameters);

            await connection.ExecuteAsync(
                sql: storedProcedureName,
                param: p,
                commandType: CommandType.StoredProcedure);

            StoredProcedureResult result = new()
            {
                ReturnValue = GetReturnValue(p),
            };

            return result;
        }

        private async ValueTask<StoredProcedureResult<T>> QueryStoredProcedureSingleResultAsync<T>(
            string storedProcedureName,
            dynamic parameters)
        {
            DynamicParameters p = StoredProcedureParameters(parameters);

            StoredProcedureResult<T> result = new()
            {
                Result = await connection.QuerySingleOrDefaultAsync<T>(
                    sql: storedProcedureName,
                    param: p,
                    commandType: CommandType.StoredProcedure),
                ReturnValue = GetReturnValue(p)
            };

            return result;
        }
    }
}

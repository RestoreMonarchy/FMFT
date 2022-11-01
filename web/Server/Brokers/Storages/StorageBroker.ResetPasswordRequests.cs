using Dapper;
using FMFT.Web.Server.Models.Database;
using FMFT.Web.Server.Models.ResetPasswordRequests;
using FMFT.Web.Server.Models.ResetPasswordRequests.Params;
using FMFT.Web.Server.Models.Users;
using System.Data;

namespace FMFT.Web.Server.Brokers.Storages
{
    public partial class StorageBroker
    {
        public async ValueTask<StoredProcedureResult<ResetPasswordRequest>> CreateResetPasswordRequestAsync(CreateResetPasswordRequestParams @params)
        {
            const string sql = "dbo.CreateResetPasswordRequest";

            DynamicParameters p = StoredProcedureParameters(@params);
            StoredProcedureResult<ResetPasswordRequest> result = new();

            await connection.QueryAsync<ResetPasswordRequest, UserInfo, ResetPasswordRequest>(sql, (r, u) =>
            {
                result.Result = r;
                result.Result.User = u;
                return null;
            }, p, commandType: CommandType.StoredProcedure);
            result.ReturnValue = GetReturnValue(p);

            return result;
        }

        public async ValueTask<ResetPasswordRequest> GetResetPasswordRequestAsync(Guid secretKey)
        {
            const string sql = "SELECT r.*, u.* FROM dbo.ResetPasswordRequests r " +
                "JOIN dbo.Users u ON u.Id = r.UserId " +
                "WHERE r.SecretKey = @secretKey AND r.IsReset = 0 AND r.IsExpired = 0;";

            ResetPasswordRequest request = null;
            await connection.QueryAsync<ResetPasswordRequest, UserInfo, ResetPasswordRequest>(sql, (r, u) =>
            {
                request = r;
                request.User = u;
                return r;
            }, new { secretKey });

            return request;
        }

        public async ValueTask<StoredProcedureResult> ExecuteResetPasswordAsync(ResetPasswordParams @params)
        {
            const string sql = "dbo.ResetPassword"
                ;
            return await ExecuteStoredProcedureAsync(sql, @params);
        }
    }
}

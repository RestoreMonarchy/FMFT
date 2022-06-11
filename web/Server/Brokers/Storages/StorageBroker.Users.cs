using Dapper;
using FMFT.Web.Server.Models.Database;
using FMFT.Web.Shared.Models.Users;
using FMFT.Web.Shared.Models.Users.Params;

namespace FMFT.Web.Server.Brokers.Storages
{
    public partial class StorageBroker
    {
        public async ValueTask<User> SelectUserByIdAsync(int userId)
        {
            const string sql = "SELECT * FROM dbo.Users WHERE Id = @userId;";
            return await connection.QuerySingleOrDefaultAsync<User>(sql, new { userId });
        }

        public async ValueTask<IEnumerable<User>> SelectAllUsersAsync()
        {
            const string sql = "SELECT * FROM dbo.Users;";
            return await connection.QueryAsync<User>(sql);
        }

        public async ValueTask<StoredProcedureResult<User>> RegisterUserWithPasswordAsync(
            RegisterUserWithPasswordParams @params)
        {
            const string sql = "dbo.RegisterUserWithPassword";
            return await QueryStoredProcedureSingleResultAsync<User>(sql, @params);
        }

        public async ValueTask<StoredProcedureResult<User>> RegisterUserWithLoginAsync(
            RegisterUserWithLoginParams @params)
        {
            const string sql = "dbo.RegisterUserWithLogin";
            return await QueryStoredProcedureSingleResultAsync<User>(sql, @params);
        }
    }
}

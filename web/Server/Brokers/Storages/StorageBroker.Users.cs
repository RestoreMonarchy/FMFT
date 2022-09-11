using Dapper;
using FMFT.Web.Server.Models.Database;
using FMFT.Web.Server.Models.Users;
using FMFT.Web.Server.Models.Users.Params;

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

        public async ValueTask<User> SelectUserByEmailAsync(string email)
        {
            const string sql = "SELECT * FROM dbo.Users WHERE Email = @email;";
            return await connection.QuerySingleOrDefaultAsync<User>(sql, new { email });
        }

        public async ValueTask<User> SelectUserByLoginAsync(string providerName, string providerKey)
        {
            const string sql = @"SELECT * FROM dbo.Users u 
                JOIN dbo.UserLogins ul ON ul.UserId = u.Id 
                WHERE ul.ProviderName = @providerName AND ul.ProviderKey = @providerKey";

            return await connection.QuerySingleOrDefaultAsync<User>(sql, new { providerName, providerKey });
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

        public async ValueTask<StoredProcedureResult> UpdateUserRoleAsync(UpdateUserRoleParams @params)
        {
            const string sql = "dbo.UpdateUserRole";
            return await ExecuteStoredProcedureAsync(sql, @params);
        }

        public async ValueTask<StoredProcedureResult> UpdateUserCultureAsync(UpdateUserCultureParams @params)
        {
            const string sql = "dbo.UpdateUserCulture";
            return await ExecuteStoredProcedureAsync(sql, @params);
        }
    }
}

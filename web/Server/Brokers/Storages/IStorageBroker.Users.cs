using FMFT.Web.Server.Models.Database;
using FMFT.Web.Server.Models.Users;
using FMFT.Web.Server.Models.Users.Params;

namespace FMFT.Web.Server.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<StoredProcedureResult<User>> RegisterUserWithPasswordAsync(RegisterUserWithPasswordParams @params);
        ValueTask<StoredProcedureResult<User>> RegisterUserWithLoginAsync(RegisterUserWithLoginParams @params);
        ValueTask<User> SelectUserByIdAsync(int userId);
        ValueTask<IEnumerable<User>> SelectAllUsersAsync();
        ValueTask<User> SelectUserByEmailAsync(string email);
        ValueTask<User> SelectUserByLoginAsync(string providerName, string providerKey);
        ValueTask<StoredProcedureResult> UpdateUserRoleAsync(UpdateUserRoleParams @params);
        ValueTask<StoredProcedureResult> UpdateUserCultureAsync(UpdateUserCultureParams @params);
        ValueTask<StoredProcedureResult> UpdateUserPasswordAsync(UpdateUserPasswordParams @params);
        ValueTask<StoredProcedureResult> ConfirmEmailAsync(int userId, Guid confirmSecret);
    }
}

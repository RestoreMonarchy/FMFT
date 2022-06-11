using FMFT.Web.Server.Models.Database;
using FMFT.Web.Shared.Models.Users;
using FMFT.Web.Shared.Models.Users.Params;

namespace FMFT.Web.Server.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<StoredProcedureResult<User>> RegisterUserWithPasswordAsync(RegisterUserWithPasswordParams @params);
        ValueTask<StoredProcedureResult<User>> RegisterUserWithLoginAsync(RegisterUserWithLoginParams @params);
        ValueTask<User> SelectUserByIdAsync(int userId);
        ValueTask<IEnumerable<User>> SelectAllUsersAsync();
        ValueTask<User> SelectUserByEmailAsync(string email);
    }
}

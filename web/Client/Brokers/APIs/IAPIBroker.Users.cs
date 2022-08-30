using FMFT.Web.Client.Models.Users;

namespace FMFT.Web.Client.Brokers.APIs
{
    public partial interface IAPIBroker
    {
        ValueTask<List<User>> GetAllUsersAsync();
        ValueTask<User> GetUserByIdAsync(int userId);
    }
}

using FMFT.Web.Client.Models.Users;

namespace FMFT.Web.Client.Services.Foundations.Users
{
    public interface IUserService
    {
        ValueTask<List<User>> RetrieveAllUsersAsync();
        ValueTask<User> RetrieveUserByIdAsync(int userId);
    }
}
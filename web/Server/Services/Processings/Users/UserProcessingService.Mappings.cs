using FMFT.Web.Shared.Models.Users;

namespace FMFT.Web.Server.Services.Processings.Users
{
    public partial class UserProcessingService
    {
        private UserInfo MapUserToUserInfo(User user)
        {
            return new UserInfo()
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role
            };
        }
    }
}

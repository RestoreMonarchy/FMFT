using FMFT.Web.Server.Models.Users;

namespace FMFT.Web.Server.Services.Processings.Users
{
    public partial class UserProcessingService
    {
        public UserInfo MapUserToUserInfo(User user)
        {
            return new UserInfo()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = user.Role
            };
        }

        public IEnumerable<UserInfo> MapUsersToUserInfos(IEnumerable<User> users)
        {
            List<UserInfo> userInfos = new();
            foreach (User user in users)
            {
                UserInfo userInfo = MapUserToUserInfo(user);
                userInfos.Add(userInfo);
            }
            return userInfos;
        }
    }
}

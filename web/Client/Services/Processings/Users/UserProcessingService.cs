using FMFT.Web.Client.Models.Users.Requests;
using FMFT.Web.Client.Services.Foundations.Users;
using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Client.Services.Processings.Users
{
    public class UserProcessingService : IUserProcessingService
    {
        private readonly IUserService userService;

        public UserProcessingService(IUserService userService)
        {
            this.userService = userService;
        }

        public async ValueTask UpdateUserCultureAsync(int userId, CultureId cultureId)
        {
            UpdateUserCultureRequest request = new()
            {
                UserId = userId,
                CultureId = cultureId
            };
            await userService.UpdateUserCultureAsync(request);
        }
    }
}

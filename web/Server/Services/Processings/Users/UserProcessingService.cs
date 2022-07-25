using FMFT.Extensions.Authentication.Models;
using FMFT.Extensions.Authentication.Models.Exceptions;
using FMFT.Web.Server.Brokers.Authentications;
using FMFT.Web.Server.Brokers.Encryptions;
using FMFT.Web.Server.Brokers.Urls;
using FMFT.Web.Server.Brokers.Validations;
using FMFT.Web.Server.Services.Foundations.Users;
using FMFT.Web.Shared.Models.Users;
using FMFT.Web.Shared.Models.Users.Exceptions;
using FMFT.Web.Shared.Models.Users.Models;
using FMFT.Web.Shared.Models.Users.Params;
using System.Security.Claims;

namespace FMFT.Web.Server.Services.Processings.Users
{
    public partial class UserProcessingService : IUserProcessingService
    {
        private readonly IUserService userService;
        private readonly IAuthenticationBroker authenticationBroker;
        private readonly IEncryptionBroker encryptionBroker;
        private readonly IValidationBroker validationBroker;
        private readonly IUrlBroker urlBroker;

        public UserProcessingService(
            IUserService userService,
            IAuthenticationBroker authenticationBroker,
            IEncryptionBroker encryptionBroker,
            IValidationBroker validationBroker,
            IUrlBroker urlBroker)
        {
            this.userService = userService;
            this.authenticationBroker = authenticationBroker;
            this.encryptionBroker = encryptionBroker;
            this.validationBroker = validationBroker;
            this.urlBroker = urlBroker;
        }

        public bool IsUserAuthenticated() 
            => userService.IsUserAuthenticated();
        public int RetrieveAuthenticatedUserId() 
            => userService.RetrieveAuthenticatedUserId();

        public void AuthorizeAuthenticatedUser()
        {
            if (!IsUserAuthenticated())
            {
                throw new UserNotAuthorizedException();
            }
        }

        public void AuthorizeAuthenticatedUserById(int userId)
        {
            AuthorizeAuthenticatedUser();
            if (RetrieveAuthenticatedUserId() != userId)
            {
                throw new UserNotAuthorizedException();
            }
        }

        public async ValueTask AuthorizeAuthenticatedUserByRolesAsync(params UserRole[] userRoles)
        {
            AuthorizeAuthenticatedUser();
            User user = await RetrieveAuthenticatedUserAsync();
            if (!userRoles.Contains(user.Role))
            {
                throw new UserNotAuthorizedException();
            }
        }

        public async ValueTask AuthorizeAuthenticatedUserByIdOrRolesAsync(int userId, params UserRole[] userRoles)
        {
            AuthorizeAuthenticatedUser();
            if (RetrieveAuthenticatedUserId() == userId)
            {
                return;
            }

            User user = await RetrieveAuthenticatedUserAsync();
            if (userRoles.Contains(user.Role))
            {
                return;
            }

            throw new UserNotAuthorizedException();
        }

        public async ValueTask<User> RetrieveAuthenticatedUserAsync()
        {
            int userId = userService.RetrieveAuthenticatedUserId();            
            return await userService.RetrieveUserByIdAsync(userId);
        }

        public async ValueTask<UserInfo> RetrieveAuthenticatedUserInfoAsync()
        {
            User user = await RetrieveAuthenticatedUserAsync();
            return MapUserToUserInfo(user);
        }
    }
}

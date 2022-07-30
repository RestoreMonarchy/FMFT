using FMFT.Web.Server.Services.Processings.Accounts;
using FMFT.Web.Server.Services.Processings.Users;

namespace FMFT.Web.Server.Services.Orchestrations.UserAccounts
{
    public partial class UserAccountOrchestrationService : IUserAccountOrchestrationService
    {
        private readonly IAccountProcessingService accountService;
        private readonly IUserProcessingService userService;

        public UserAccountOrchestrationService(IAccountProcessingService accountService, IUserProcessingService userService)
        {
            this.accountService = accountService;
            this.userService = userService;
        }

        public async ValueTask RegisterUserWithPasswordAsync()
        {
            if (validationBroker.IsPasswordInvalid(model.PasswordText))
            {
                throw new UserPasswordInvalidException();
            }

            RegisterUserWithPasswordParams @params = new()
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PasswordHash = encryptionBroker.HashPassword(model.PasswordText),
                Role = UserRole.Guest
            };

            User user = await userService.RegisterUserWithPasswordAsync(@params);
            await SignInUserAsync(user, false, null);
            return MapUserToUserInfo(user);
        }
    }
}

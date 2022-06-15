using FMFT.Web.Client.Services.Foundations.Accounts;
using FMFT.Web.Shared.Models.Users;
using FMFT.Web.Shared.Models.Users.Exceptions;
using FMFT.Web.Shared.Models.Users.Models;

namespace FMFT.Web.Client.Services.Processings.Accounts
{
    public class AccountProcessingService : IAccountProcessingService
    {
        private readonly IAccountService accountService;

        public AccountProcessingService(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public UserInfo UserInfo { get; private set; }
        public bool IsAuthenticated => UserInfo != null;

        public event Action UserInfoChanged;

        public async ValueTask InitializeAsync()
        {
            await ReloadUserInfoAsync();
        }

        private void ChangeUserInfo(UserInfo userInfo)
        {
            UserInfo = userInfo;
            UserInfoChanged?.Invoke();
        }

        public async ValueTask ReloadUserInfoAsync()
        {
            UserInfo userInfo;
            try
            {
                userInfo = await accountService.RetrieveAccountInfoAsync();
            }
            catch (UserNotAuthenticatedException)
            {
                userInfo = null;
            }

            ChangeUserInfo(userInfo);
        }


        public async ValueTask LoginAsync(SignInUserWithPasswordModel model)
        {
            UserInfo userInfo = await accountService.LoginAsync(model);
            ChangeUserInfo(userInfo);
        }

        public async ValueTask RegisterAsync(RegisterUserWithPasswordModel model)
        {
            UserInfo userInfo = await accountService.RegisterAsync(model);
            ChangeUserInfo(userInfo);
        }
    }
}

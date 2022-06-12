using FMFT.Web.Client.Services.Foundations.Accounts;
using FMFT.Web.Shared.Models.Users.Models;

namespace FMFT.Web.Client.Services.Views.Accounts
{
    public class AccountViewService : IAccountViewService
    {
        private readonly IAccountService accountService;

        public AccountViewService(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public async ValueTask LoginAsync(SignInUserWithPasswordModel model) 
            => await accountService.LoginAsync(model);

        public async ValueTask RegisterAsync(RegisterUserWithPasswordModel model)
            => await accountService.RegisterAsync(model);
    }
}

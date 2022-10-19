using FMFT.Web.Client.Models.API.Accounts;
using FMFT.Web.Client.Services.Accounts;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Pages.Account
{
    public partial class LoginPage
    {
        [Inject]
        public IAccountService AccountService { get; set; }

        private async Task OnSuccessfullLoginAsync(AccountToken accountToken)
        {
            await AccountService.LoginAsync(accountToken);
            NavigationBroker.NavigateTo("/");
        }
    }
}

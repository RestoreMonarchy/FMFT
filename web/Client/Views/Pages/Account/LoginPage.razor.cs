using FMFT.Extensions.Blazor.Facebook;
using FMFT.Web.Client.Brokers.ExternalLogins;
using FMFT.Web.Client.Models.API.Accounts;
using FMFT.Web.Client.Services.Accounts;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Pages.Account
{
    public partial class LoginPage
    {
        [Inject]
        public IAccountService AccountService { get; set; }
        [Inject]
        public IExternalLoginBroker ExternalLoginBroker { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await ExternalLoginBroker.InitializeFacebookAsync();
        }

        private async Task HandleSuccessfullLoginAsync(AccountToken accountToken)
        {
            await AccountService.LoginAsync(accountToken);
            NavigationBroker.NavigateTo("/");
        }

        private async Task HandleFacebookLoginAsync()
        {
            await ExternalLoginBroker.LoginFacebookAsync();
        }
    }
}

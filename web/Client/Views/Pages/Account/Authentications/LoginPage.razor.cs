using FMFT.Extensions.Blazor.Facebook;
using FMFT.Web.Client.Brokers.ExternalLogins;
using FMFT.Web.Client.Models.API.Accounts;
using FMFT.Web.Client.Services.Accounts;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Pages.Account.Authentications
{
    public partial class LoginPage
    {
        [Inject]
        public IAccountService AccountService { get; set; }
        [Inject]
        public IExternalLoginBroker ExternalLoginBroker { get; set; }

        private bool IsFacebookDisabled = false;
        private bool IsGoogleDisabled = false;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                await ExternalLoginBroker.InitializeFacebookAsync();
            } catch (Exception e)
            {
                LoggingBroker.LogError(e, "Failed to initialize facebook script");
                IsFacebookDisabled = true;
            }
            
            try
            {
                await ExternalLoginBroker.InitializeGoogleAsync();
            } catch (Exception e)
            {
                LoggingBroker.LogError(e, "Failed to initialize google script");
                IsGoogleDisabled = true;
            }            
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

        private async Task HandleGoogleLoginAsync()
        {
            await ExternalLoginBroker.LoginGoogleAsync();
        }
    }
}

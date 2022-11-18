using FMFT.Extensions.Blazor.Facebook.Models.Results;
using FMFT.Web.Client.Brokers.APIs;
using FMFT.Web.Client.Brokers.Loggings;
using FMFT.Web.Client.Brokers.Navigations;
using FMFT.Web.Client.Brokers.Storages;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Accounts;
using FMFT.Web.Client.Models.API.Accounts.Requests;
using FMFT.Web.Client.StateContainers.UserAccounts;

namespace FMFT.Web.Client.Services.Accounts
{
    public class AccountService : IAccountService
    {
        private readonly IAPIBroker apiBroker;
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;
        private readonly IUserAccountStateContainer userAccountStateContainer;
        private readonly INavigationBroker navigationBroker;

        public AccountService(
            IAPIBroker apiBroker,
            IStorageBroker storageBroker,
            ILoggingBroker loggingBroker,
            IUserAccountStateContainer userAccountStateContainer,
            INavigationBroker navigationBroker)
        {
            this.storageBroker = storageBroker;
            this.apiBroker = apiBroker;
            this.loggingBroker = loggingBroker;
            this.userAccountStateContainer = userAccountStateContainer;
            this.navigationBroker = navigationBroker;
        }

        public async ValueTask InitializeAsync()
        {
            AccountToken accountToken = await storageBroker.GetAccountTokenAsync();

            if (accountToken == null)
            {
                loggingBroker.LogDebug("Account token not found in local storage");
                return;
            }

            apiBroker.SetAuthorizationToken(accountToken.Token);
            bool isSuccessfull = await UpdateUserAccountAsync();
            if (!isSuccessfull)
            {
                await LogoutAsync();
            }
        }

        public async ValueTask LoginAsync(AccountToken accountToken)
        {
            if (accountToken == null)
            {
                throw new ArgumentNullException(nameof(accountToken));
            }

            await storageBroker.SetAccountTokenAsync(accountToken);
            apiBroker.SetAuthorizationToken(accountToken.Token);
            bool isSuccessfull = await UpdateUserAccountAsync();
            if (!isSuccessfull)
            {
                await LogoutAsync();
            }
        }

        public async ValueTask LogoutAsync()
        {
            await storageBroker.SetAccountTokenAsync(null);
            apiBroker.RemoveAuthorizationToken();
            userAccountStateContainer.UserAccount = null;
        }

        private async ValueTask<bool> UpdateUserAccountAsync()
        {
            APIResponse<UserAccount> response = await apiBroker.GetUserAccountAsync();
            if (response.IsSuccessful)
            {
                userAccountStateContainer.UserAccount = response.Object;
                loggingBroker.LogDebug($"Successfully downloaded user account {response.Object.Email}");
                return true;
            }
            else
            {
                loggingBroker.LogDebug($"Failed to retrieve the user account from the web api");
                return false;
            }
        }

        public async ValueTask HandleFacebookLoginAsync(FacebookLoginResult result)
        {
            if (result.Status != FacebookLoginStatus.Connected)
            {
                loggingBroker.LogDebug($"The facebook result status is: {result.Status}");
                return;
            }
            
            if (userAccountStateContainer.IsAuthenticated)
            {
                loggingBroker.LogDebug("The user is already authenticated, ignoring facebook login");
                return;
            }

            FacebookLoginRequest request = new()
            {
                AccessToken = result.AuthResponse.AccessToken
            };

            APIResponse<AccountToken> response = await apiBroker.PostAccountFacebookLoginAsync(request);

            if (!response.IsSuccessful)
            {
                navigationBroker.NavigateTo($"account/externalloginerror/{response.Error.Code}");
                loggingBroker.LogDebug(response.Error.Title);
                return;
            }

            await LoginAsync(response.Object);
            navigationBroker.NavigateTo("/");
        }
    }
}
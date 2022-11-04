﻿using FMFT.Web.Client.Brokers.APIs;
using FMFT.Web.Client.Brokers.Loggings;
using FMFT.Web.Client.Brokers.MemoryStorages;
using FMFT.Web.Client.Brokers.Storages;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Accounts;
using FMFT.Web.Client.StateContainers.UserAccounts;
using Microsoft.JSInterop;
using System.IO.IsolatedStorage;

namespace FMFT.Web.Client.Services.Accounts
{
    public class AccountService : IAccountService
    {
        private readonly IAPIBroker apiBroker;
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;
        private readonly IUserAccountStateContainer userAccountStateContainer;

        public AccountService(IAPIBroker apiBroker, IStorageBroker storageBroker, ILoggingBroker loggingBroker, IUserAccountStateContainer userAccountStateContainer)
        {
            this.storageBroker = storageBroker;
            this.apiBroker = apiBroker;
            this.loggingBroker = loggingBroker;
            this.userAccountStateContainer = userAccountStateContainer;
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
                apiBroker.SetAuthorizationToken(null);
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
                apiBroker.SetAuthorizationToken(null);
            }
        }

        public async ValueTask LogoutAsync()
        {
            await storageBroker.SetAccountTokenAsync(null);
            userAccountStateContainer.UserAccount = null;
        }

        private async ValueTask<bool> UpdateUserAccountAsync()
        {
            APIResponse<UserAccount> response = await apiBroker.GetUserAccountAsync();
            if (response.IsSuccessfull)
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
    }
}

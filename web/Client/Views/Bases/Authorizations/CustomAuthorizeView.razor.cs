using FMFT.Web.Client.Models.Accounts;
using FMFT.Web.Client.Models.Accounts.Exceptions;
using FMFT.Web.Client.Services.Processings.Accounts;
using FMFT.Web.Client.Services.Processings.AccountStores;
using FMFT.Web.Shared.Enums;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Bases.Authorizations
{
    public partial class CustomAuthorizeView
    {
        [Parameter]
        public RenderFragment<Account> Authorized { get; set; }
        [Parameter]
        public RenderFragment NotAuthorized { get; set; }
        [Parameter]
        public IEnumerable<UserRole> Roles { get; set; }
        [Parameter]
        public UserRole? Role { get; set; }

        private IEnumerable<UserRole> roles
        {
            get
            {
                if (Roles == null && Role == null)
                    return null;

                List<UserRole> roles = Roles?.ToList() ?? new List<UserRole>();

                if (Role.HasValue)
                    roles.Add(Role.Value);

                return roles;
            }
        }

        [Inject]
        public IAccountStoreProcessingService AccountStoreService { get; set; }

        public Account Account { get; set; }

        protected override void OnInitialized()
        {
            try
            {
                Account = AccountStoreService.RetrieveAccount();
            } catch (AccountNotAuthenticatedException)
            {
                Account = null;
            }            
        }

        public bool IsAuthorized()
        {
            if (Account == null)
            {
                return false;
            }

            if (roles != null && !roles.Contains(Account.Role))
            {
                return false;
            }

            return true;
        }
    }
}

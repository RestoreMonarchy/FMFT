using FMFT.Web.Client.Models.Accounts;
using FMFT.Web.Client.Services.Processings.Accounts;
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


        [Inject]
        public IAccountProcessingService AccountService { get; set; }

        public bool IsAuthorized()
        {
            if (!AccountService.IsAuthenticated)
            {
                return false;
            }

            if (Roles != null && !Roles.Contains(AccountService.Account.Role))
            {
                return false;
            }

            return true;
        }
    }
}

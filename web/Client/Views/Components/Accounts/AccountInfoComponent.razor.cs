using FMFT.Web.Client.Models.Accounts;
using FMFT.Web.Client.Services.Views.Accounts;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Components.Accounts
{
    public partial class AccountInfoComponent
    {
        [Inject]
        public IAccountViewService AccountViewService { get; set; }

        public Account Account => AccountViewService.Account;
    }
}

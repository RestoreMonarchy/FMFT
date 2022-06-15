using FMFT.Web.Client.Services.Views.Accounts;
using FMFT.Web.Shared.Models.Users;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Components.Accounts
{
    public partial class AccountInfoComponent
    {
        [Inject]
        public IAccountViewService AccountViewService { get; set; }

        public UserInfo UserInfo => AccountViewService.UserInfo;
    }
}

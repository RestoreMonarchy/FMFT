using FMFT.Web.Client.Models.API.Users;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Shared.Components.Cards.Users
{
    public partial class UserInfoAdminCard
    {
        [Parameter]
        public UserInfo UserInfo { get; set; }
    }
}

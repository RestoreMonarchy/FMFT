using FMFT.Web.Client.Models.Users;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Components.Users.Cards
{
    public partial class UserIdentityCard
    {
        [Parameter]
        public User User { get; set; }
    }
}

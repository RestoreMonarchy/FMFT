using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Components.Users.Cards
{
    public partial class UserPermissionsCard
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}

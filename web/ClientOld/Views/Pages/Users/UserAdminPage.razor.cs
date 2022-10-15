using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Pages.Users
{
    public partial class UserAdminPage
    {
        [Parameter]
        public int UserId { get; set; }
    }
}

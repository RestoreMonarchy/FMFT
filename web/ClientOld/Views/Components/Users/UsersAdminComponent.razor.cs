using FMFT.Web.Client.Models.Users;
using FMFT.Web.Client.Services.Views.Users;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Components.Users
{
    public partial class UsersAdminComponent
    {
        [Inject]
        public IUserViewService UserViewService { get; set; }


        public List<User> Users { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Users = await UserViewService.RetrieveAllUsersAsync();
        }
    }
}

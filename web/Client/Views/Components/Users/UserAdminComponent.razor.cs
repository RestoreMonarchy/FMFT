using FMFT.Web.Client.Models.Users;
using FMFT.Web.Client.Models.Users.Exceptions;
using FMFT.Web.Client.Services.Views.Users;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Components.Users
{
    public partial class UserAdminComponent
    {
        [Parameter]
        public int UserId { get; set; }

        [Inject]
        public IUserViewService UserViewService { get; set; }

        public User User { get; set; }
        public Exception Exception { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            try
            {
                User = await UserViewService.RetrieveUserByIdAsync(UserId);
            }
            catch (UserNotFoundException e)
            {
                Exception = e;
            }
        }
    }
}

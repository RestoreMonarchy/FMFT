using FMFT.Extensions.Blazor.Bases.Loadings;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Users.Requests;
using FMFT.Web.Client.Services.Accounts;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Pages.Users
{
    public partial class UserConfirmEmailPage
    {
        [Parameter]
        public int UserId { get; set; }
        [Parameter]
        public string ConfirmSecret { get; set; }

        [Inject]
        public IAccountService AccountService { get; set; }

        public LoadingSpinnerView LoadingSpinnerView { get; set; }

        public APIResponse ConfirmUserResponse { get; set; }
        
        protected override async Task OnParametersSetAsync()
        {
            ConfirmUserEmailRequest request = new()
            {
                UserId = UserId,
                ConfirmSecret = Guid.Parse(ConfirmSecret)
            };

            ConfirmUserResponse = await APIBroker.ConfirmUserEmailAsync(request);
            if (ConfirmUserResponse.IsSuccessful)
            {
                await AccountService.UpdateUserAccountAsync();
            }
            LoadingSpinnerView.StopLoading();
        }
    }
}

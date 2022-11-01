using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Pages.Account
{
    public partial class ResetPasswordPage
    {
        [Parameter]
        public string SecretKey { get; set; }

        public Guid SecretKeyId { get; set; }

        protected override void OnParametersSet()
        {
            SecretKeyId = Guid.Parse(SecretKey);
        }

        private bool isSuccessful = false;

        private Task OnSuccessfullyResetPasswordAsync()
        {
            isSuccessful = true;
            return Task.CompletedTask;
        }
    }
}

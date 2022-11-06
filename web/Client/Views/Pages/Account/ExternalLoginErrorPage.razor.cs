using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Pages.Account
{
    public partial class ExternalLoginErrorPage
    {
        [Parameter]
        public string ErrorCode { get; set; }
    }
}

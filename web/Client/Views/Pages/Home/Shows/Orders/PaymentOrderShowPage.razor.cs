using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Pages.Home.Shows.Orders
{
    public partial class PaymentOrderShowPage
    {
        [Parameter]
        public int ShowId { get; set; }

        private string GetUrl(string subPage)
        {
            return $"/shows/{ShowId}/order/{subPage}";
        }
    }
}

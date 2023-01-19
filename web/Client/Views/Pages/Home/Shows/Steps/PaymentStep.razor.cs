using FMFT.Web.Client.Models.API.Shows;
using FMFT.Web.Client.Models.Services.Orders;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Pages.Home.Shows.Steps
{
    public partial class PaymentStep
    {
        [Parameter]
        public OrderState OrderState { get; set; }
        [Parameter]
        public Show Show { get; set; }
    }
}

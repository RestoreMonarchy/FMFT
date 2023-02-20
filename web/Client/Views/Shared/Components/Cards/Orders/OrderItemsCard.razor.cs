using FMFT.Web.Client.Models.API.Orders;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Shared.Components.Cards.Orders
{
    public partial class OrderItemsCard
    {
        [Parameter]
        public Order Order { get; set; }
    }
}

using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Components.Reservations.Cards
{
    public partial class ReservationActionsCard
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}

using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Pages.Reservations
{
    public partial class ReservationAdminPage
    {
        [Parameter]
        public int ReservationId { get; set; }
    }
}

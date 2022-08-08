using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Pages.Users
{
    public partial class UserReservationPage
    {
        [Parameter]
        public int UserId { get; set; }
        [Parameter]
        public int ReservationId { get; set; }
    }
}

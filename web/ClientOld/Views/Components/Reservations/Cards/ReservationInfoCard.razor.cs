using FMFT.Web.Client.Models.Auditoriums;
using FMFT.Web.Client.Models.Reservations;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Components.Reservations.Cards
{
    public partial class ReservationInfoCard
    {
        [Parameter]
        public Reservation Reservation { get; set; }
        [Parameter]
        public Auditorium Auditorium { get; set; }
    }
}

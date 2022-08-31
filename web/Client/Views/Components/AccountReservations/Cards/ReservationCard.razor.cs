using FMFT.Web.Client.Models.Auditoriums;
using FMFT.Web.Client.Models.Reservations;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Components.AccountReservations.Cards
{
    public partial class ReservationCard
    {
        [Parameter]
        public Auditorium Auditorium { get; set; }
        [Parameter]
        public Reservation Reservation { get; set; }
    }
}

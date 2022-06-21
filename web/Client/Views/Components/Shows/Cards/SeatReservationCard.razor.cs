using FMFT.Web.Shared.Models.Auditoriums;
using FMFT.Web.Shared.Models.Seats;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Components.Shows.Cards
{
    public partial class SeatReservationCard
    {
        [Parameter]
        public Auditorium Auditorium { get; set; }

        public List<Seat> SelectedSeats { get; set; } = new();
    }
}

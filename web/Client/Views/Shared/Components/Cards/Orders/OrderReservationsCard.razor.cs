using FMFT.Web.Client.Models.API.Reservations;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Shared.Components.Cards.Orders
{
    public partial class OrderReservationsCard
    {
        [Parameter]
        public List<Reservation> OrderReservations { get; set; }
    }
}

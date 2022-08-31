using FMFT.Web.Client.Models.Reservations;

namespace FMFT.Web.Client.Services.Views.Reservations
{
    public interface IReservationViewService
    {
        ValueTask<List<Reservation>> RetrieveAllReservationsAsync();
    }
}

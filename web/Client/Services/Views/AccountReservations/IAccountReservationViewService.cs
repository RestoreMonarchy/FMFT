using FMFT.Web.Client.Models.Reservations;

namespace FMFT.Web.Client.Services.Views.AccountReservations
{
    public interface IAccountReservationViewService
    {
        ValueTask<List<Reservation>> RetrieveAccountReservationsAsync();
    }
}
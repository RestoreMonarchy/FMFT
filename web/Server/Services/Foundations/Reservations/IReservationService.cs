using FMFT.Web.Shared.Models.Reservations;
using FMFT.Web.Shared.Models.Reservations.Params;

namespace FMFT.Web.Server.Services.Foundations.Reservations
{
    public interface IReservationService
    {
        ValueTask<Reservation> CreateReservationAsync(CreateReservationParams @params);
        ValueTask<IEnumerable<Reservation>> RetrieveAllReservationsAsync();
        ValueTask<Reservation> RetrieveReservationByIdAsync(int reservationId);
    }
}
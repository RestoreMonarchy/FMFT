using FMFT.Web.Shared.Models.Reservations;
using FMFT.Web.Shared.Models.Reservations.Models;

namespace FMFT.Web.Client.Services.Processings.Reservations
{
    public interface IReservationProcessingService
    {
        ValueTask<Reservation> CreateReservationAsync(CreateReservationModel model);
    }
}
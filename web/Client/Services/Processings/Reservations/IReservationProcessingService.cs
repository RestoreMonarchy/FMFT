using FMFT.Web.Server.Models.Reservations;
using FMFT.Web.Server.Models.Reservations.Models;

namespace FMFT.Web.Client.Services.Processings.Reservations
{
    public interface IReservationProcessingService
    {
        ValueTask<Reservation> CreateReservationAsync(CreateReservationModel model);
    }
}
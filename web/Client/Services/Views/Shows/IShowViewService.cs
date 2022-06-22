using FMFT.Web.Shared.Models.Auditoriums;
using FMFT.Web.Shared.Models.Reservations;
using FMFT.Web.Shared.Models.Reservations.Models;
using FMFT.Web.Shared.Models.Shows;

namespace FMFT.Web.Client.Services.Views.Shows
{
    public interface IShowViewService
    {
        ValueTask<Reservation> CreateReservationAsync(CreateReservationModel model);
        ValueTask<List<Show>> RetrieveAllShowsAsync();
        ValueTask<Auditorium> RetrieveAuditoriumAsync(int auditoriumId);
        ValueTask<Show> RetrieveShowByIdAsync(int showId);
    }
}
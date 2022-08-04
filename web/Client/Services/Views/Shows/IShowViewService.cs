using FMFT.Web.Server.Models.Auditoriums;
using FMFT.Web.Server.Models.Reservations;
using FMFT.Web.Server.Models.Reservations.Models;
using FMFT.Web.Server.Models.Shows;

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
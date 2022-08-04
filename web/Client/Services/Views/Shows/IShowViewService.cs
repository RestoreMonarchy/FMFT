using FMFT.Web.Client.Models.Auditoriums;
using FMFT.Web.Client.Models.Reservations;
using FMFT.Web.Client.Models.Reservations.Requests;
using FMFT.Web.Client.Models.Shows;

namespace FMFT.Web.Client.Services.Views.Shows
{
    public interface IShowViewService
    {
        ValueTask<Reservation> CreateReservationAsync(CreateReservationRequest request);
        ValueTask<List<Show>> RetrieveAllShowsAsync();
        ValueTask<Auditorium> RetrieveAuditoriumByIdAsync(int auditoriumId);
        ValueTask<Show> RetrieveShowByIdAsync(int showId);
    }
}
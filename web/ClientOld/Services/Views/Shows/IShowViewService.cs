using FMFT.Web.Client.Models.AccountReservations.Arguments;
using FMFT.Web.Client.Models.Auditoriums;
using FMFT.Web.Client.Models.Reservations;
using FMFT.Web.Client.Models.Reservations.Requests;
using FMFT.Web.Client.Models.Shows;
using FMFT.Web.Client.Models.Shows.Params;

namespace FMFT.Web.Client.Services.Views.Shows
{
    public interface IShowViewService
    {
        ValueTask<Show> AddShowAsync(AddShowParams @params);
        ValueTask<Reservation> CreateAccountReservationAsync(CreateAccountReservationArguments arguments);
        void NavigateTo(string url);
        ValueTask<List<Auditorium>> RetrieveAllAuditoriumsAsync();
        ValueTask<List<Show>> RetrieveAllShowsAsync();
        ValueTask<Auditorium> RetrieveAuditoriumByIdAsync(int auditoriumId);
        ValueTask<Show> RetrieveShowByIdAsync(int showId);
        ValueTask<Show> UpdateShowAsync(UpdateShowParams @params);
    }
}
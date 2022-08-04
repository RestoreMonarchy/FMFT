using FMFT.Web.Server.Models.Auditoriums;

namespace FMFT.Web.Server.Services.Foundations.Auditoriums
{
    public interface IAuditoriumService
    {
        ValueTask<IEnumerable<Auditorium>> RetrieveAllAuditoriumsAsync();
        ValueTask<Auditorium> RetrieveAuditoriumByIdAsync(int auditoriumId);
    }
}

using FMFT.Web.Server.Models.Auditoriums;

namespace FMFT.Web.Client.Services.Foundations.Auditoriums
{
    public interface IAuditoriumService
    {
        ValueTask<List<Auditorium>> RetrieveAllAuditoriumsAsync();
        ValueTask<Auditorium> RetrieveAuditoriumByIdAsync(int auditoriumId);
    }
}

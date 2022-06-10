using FMFT.Web.Shared.Models.Auditoriums;

namespace FMFT.Web.Client.Brokers.APIs
{
    public partial interface IAPIBroker
    {
        ValueTask<Auditorium> GetAuditoriumByIdAsync(int auditoriumId);
        ValueTask<List<Auditorium>> GetAllAuditoriumsAsync();
    }
}

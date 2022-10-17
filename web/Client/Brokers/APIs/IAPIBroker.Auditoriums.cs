using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Auditoriums;

namespace FMFT.Web.Client.Brokers.APIs
{
    public partial interface IAPIBroker
    {
        ValueTask<APIResponse<Auditorium>> GetAuditoriumByIdAsync(int auditoriumId);
        ValueTask<APIResponse<List<Auditorium>>> GetAllAuditoriumsAsync();
    }
}

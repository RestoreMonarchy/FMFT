using FMFT.Web.Shared.Models.Auditoriums;

namespace FMFT.Web.Server.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Auditorium> SelectAuditoriumByIdAsync(int auditoriumId);
        ValueTask<IEnumerable<Auditorium>> SelectAllAuditoriumsAsync();
    }
}

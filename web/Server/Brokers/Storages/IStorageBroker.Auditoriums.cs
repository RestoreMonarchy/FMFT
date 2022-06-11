using FMFT.Web.Server.Models.Database;
using FMFT.Web.Shared.Models.Auditoriums;
using FMFT.Web.Shared.Models.Users;
using FMFT.Web.Shared.Models.Users.Params;

namespace FMFT.Web.Server.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Auditorium> SelectAuditoriumByIdAsync(int auditoriumId);
        ValueTask<IEnumerable<Auditorium>> SelectAllAuditoriumsAsync();
    }
}

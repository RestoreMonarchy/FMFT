using FMFT.Web.Server.Models.Database;
using FMFT.Web.Server.Models.Auditoriums;
using FMFT.Web.Server.Models.Users;
using FMFT.Web.Server.Models.Users.Params;

namespace FMFT.Web.Server.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Auditorium> SelectAuditoriumByIdAsync(int auditoriumId);
        ValueTask<IEnumerable<Auditorium>> SelectAllAuditoriumsAsync();
    }
}

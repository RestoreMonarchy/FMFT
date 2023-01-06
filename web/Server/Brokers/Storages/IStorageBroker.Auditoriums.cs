using FMFT.Web.Server.Models.Auditoriums;
using FMFT.Web.Server.Models.ShowProducts;
using FMFT.Web.Server.Models.ShowProducts.Params;

namespace FMFT.Web.Server.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Auditorium> SelectAuditoriumByIdAsync(int auditoriumId);
        ValueTask<IEnumerable<Auditorium>> SelectAllAuditoriumsAsync();
    }
}

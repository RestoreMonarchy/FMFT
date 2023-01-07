using FMFT.Web.Server.Models.ShowProducts;
using FMFT.Web.Server.Models.ShowProducts.Params;

namespace FMFT.Web.Server.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<IEnumerable<ShowProduct>> SelectShowProductsByShowIdAsync(int showId);
        ValueTask<ShowProduct> SelectShowProductByIdAsync(int showProductId);
        ValueTask<ShowProduct> InsertShowProductAsync(AddShowProductParams @params);
        ValueTask<ShowProduct> UpdateShowProductAsync(UpdateShowProductParams @params);
    }
}

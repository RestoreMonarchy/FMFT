using FMFT.Web.Server.Models.ShowProducts;
using FMFT.Web.Server.Models.ShowProducts.Params;

namespace FMFT.Web.Server.Services.Foundations.ShowProducts
{
    public interface IShowProductService
    {
        ValueTask<ShowProduct> AddShowProductAsync(AddShowProductParams @params);
        ValueTask<IEnumerable<ShowProduct>> RetrieveShowProductsByShowIdAsync(int showId);
        ValueTask<ShowProduct> ModifyShowProductAsync(UpdateShowProductParams @params);
        ValueTask RemoveShowProductByIdAndShowIdAsync(int showProductId, int showId);
    }
}
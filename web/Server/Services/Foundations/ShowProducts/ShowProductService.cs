using FMFT.Web.Server.Brokers.Storages;
using FMFT.Web.Server.Models.ShowProducts;
using FMFT.Web.Server.Models.ShowProducts.Exceptions;
using FMFT.Web.Server.Models.ShowProducts.Params;

namespace FMFT.Web.Server.Services.Foundations.ShowProducts
{
    public class ShowProductService : IShowProductService
    {
        private readonly IStorageBroker storageBroker;

        public ShowProductService(IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;
        }

        public async ValueTask<IEnumerable<ShowProduct>> RetrieveShowProductsByShowIdAsync(int showId)
        {
            return await storageBroker.SelectShowProductsByShowIdAsync(showId);
        }

        public async ValueTask<ShowProduct> AddShowProductAsync(AddShowProductParams @params)
        {
            return await storageBroker.InsertShowProductAsync(@params);
        }

        public async ValueTask<ShowProduct> ModifyShowProductAsync(UpdateShowProductParams @params)
        {
            return await storageBroker.UpdateShowProductAsync(@params);
        }

        public async ValueTask RemoveShowProductByIdAndShowIdAsync(int showProductId, int showId)
        {
            bool isDeleted = await storageBroker.DeleteShowProductByIdAndShowIdAsync(showProductId, showId);

            if (!isDeleted)
            {
                throw new NotFoundShowProductException();
            }
        }
    }
}

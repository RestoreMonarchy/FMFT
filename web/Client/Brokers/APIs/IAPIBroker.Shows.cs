using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.ShowProducts;
using FMFT.Web.Client.Models.API.ShowProducts.Requests;
using FMFT.Web.Client.Models.API.Shows;
using FMFT.Web.Client.Models.API.Shows.Requests;

namespace FMFT.Web.Client.Brokers.APIs
{
    public partial interface IAPIBroker
    {
        ValueTask<APIResponse<Show>> GetShowByIdAsync(int showId);
        ValueTask<APIResponse<List<Show>>> GetAllShowsAsync();
        ValueTask<APIResponse<Show>> GetPublicShowByIdAsync(int showId);
        ValueTask<APIResponse<List<Show>>> GetPublicShowsAsync();
        ValueTask<APIResponse<Show>> AddShowAsync(AddShowRequest request);
        ValueTask<APIResponse<Show>> UpdateShowAsync(UpdateShowRequest request);
        ValueTask<APIResponse<Show>> UpdateShowSellingDetailsAsync(UpdateShowSellingDetailsRequest request);
        ValueTask<APIResponse<Show>> UpdateShowStatusAsync(UpdateShowStatusRequest request);
        ValueTask<APIResponse<Show>> UpdateShowTimeAsync(UpdateShowTimeRequest request);

        ValueTask<APIResponse> AddShowGalleryAsync(AddShowGalleryRequest request);
        ValueTask<APIResponse<List<ShowGallery>>> GetShowGalleryByShowIdAsync(int showId);
        ValueTask<APIResponse> DeleteShowGalleryByIdAsync(int showGalleryId);

        ValueTask<APIResponse<List<ShowProduct>>> GetShowProductsByShowIdAsync(int showId);
        ValueTask<APIResponse<ShowProduct>> AddShowProductAsync(AddShowProductRequest request);
        ValueTask<APIResponse<ShowProduct>> ModifyShowProductAsync(ModifyShowProductRequest request);
        ValueTask<APIResponse> DeleteShowProductAsync(int showId, int showProductId);
    }
}

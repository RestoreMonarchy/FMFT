using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.ShowProducts;
using FMFT.Web.Client.Models.API.Shows;
using FMFT.Web.Client.Models.API.Shows.Requests;

namespace FMFT.Web.Client.Brokers.APIs
{
    public partial interface IAPIBroker
    {
        ValueTask<APIResponse<Show>> GetShowByIdAsync(int showId);
        ValueTask<APIResponse<List<Show>>> GetAllShowsAsync();
        ValueTask<APIResponse<Show>> AddShowAsync(AddShowRequest request);
        ValueTask<APIResponse<Show>> UpdateShowAsync(UpdateShowRequest request);

        ValueTask<APIResponse> AddShowGalleryAsync(AddShowGalleryRequest request);
        ValueTask<APIResponse<List<ShowGallery>>> GetShowGalleryByShowIdAsync(int showId);
        ValueTask<APIResponse> DeleteShowGalleryByIdAsync(int showGalleryId);

        ValueTask<APIResponse<List<ShowProduct>>> GetShowProductsByShowIdAsync(int showId);
    }
}

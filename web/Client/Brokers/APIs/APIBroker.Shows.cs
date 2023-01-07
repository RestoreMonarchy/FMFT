using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.ShowProducts;
using FMFT.Web.Client.Models.API.ShowProducts.Requests;
using FMFT.Web.Client.Models.API.Shows;
using FMFT.Web.Client.Models.API.Shows.Requests;

namespace FMFT.Web.Client.Brokers.APIs
{
    public partial class APIBroker
    {
        private const string ShowsRelativeUrl = "api/shows";

        public async ValueTask<APIResponse<Show>> GetShowByIdAsync(int showId)
        {
            return await GetAsync<Show>($"{ShowsRelativeUrl}/{showId}");
        }

        public async ValueTask<APIResponse<List<Show>>> GetAllShowsAsync()
        {
            return await GetAsync<List<Show>>(ShowsRelativeUrl);
        }

        public async ValueTask<APIResponse<Show>> AddShowAsync(AddShowRequest request)
        {
            return await PostAsync<Show>(ShowsRelativeUrl, request);
        }

        public async ValueTask<APIResponse<Show>> UpdateShowAsync(UpdateShowRequest request)
        {
            return await PutAsync<Show>(ShowsRelativeUrl, request);
        }

        public async ValueTask<APIResponse> AddShowGalleryAsync(AddShowGalleryRequest request)
        {
            string url = $"{ShowsRelativeUrl}/{request.ShowId}/gallery/add";

            return await PostAsync(url, request);
        }

        public async ValueTask<APIResponse<List<ShowGallery>>> GetShowGalleryByShowIdAsync(int showId)
        {
            string url = $"{ShowsRelativeUrl}/{showId}/gallery";

            return await GetAsync<List<ShowGallery>>(url);
        }

        public async ValueTask<APIResponse> DeleteShowGalleryByIdAsync(int showGalleryId)
        {
            string url = $"{ShowsRelativeUrl}/gallery/{showGalleryId}/delete";

            return await DeleteAsync(url);
        }

        public async ValueTask<APIResponse<List<ShowProduct>>> GetShowProductsByShowIdAsync(int showId)
        {
            string url = $"{ShowsRelativeUrl}/{showId}/products";

            return await GetAsync<List<ShowProduct>>(url);
        }

        public async ValueTask<APIResponse<ShowProduct>> AddShowProductAsync(AddShowProductRequest request)
        {
            string url = $"{ShowsRelativeUrl}/{request.ShowId}/products/add";

            return await PostAsync<ShowProduct>(url, request);
        }
    }
}

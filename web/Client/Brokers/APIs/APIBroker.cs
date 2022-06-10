using RESTFulSense.WebAssembly.Clients;

namespace FMFT.Web.Client.Brokers.APIs
{
    public partial class APIBroker : IAPIBroker
    {
        private readonly HttpClient httpClient;
        private readonly IRESTFulApiFactoryClient apiClient;        

        public APIBroker(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            apiClient = GetApiClient();
        }

        private async ValueTask<T> GetAsync<T>(string relativeUrl)
        {
            return await apiClient.GetContentAsync<T>(relativeUrl);
        }

        private async ValueTask<T> PostAsync<T>(string relativeUrl, T content)
        {
            return await apiClient.PostContentAsync<T>(relativeUrl, content);
        }

        private async ValueTask<T> PutAsync<T>(string relativeUrl, T content)
        {
            return await apiClient.PutContentAsync<T>(relativeUrl, content);
        }

        private async ValueTask<T> DeleteAsync<T>(string relativeUrl)
        {
            return await apiClient.DeleteContentAsync<T>(relativeUrl);
        }

        private IRESTFulApiFactoryClient GetApiClient()
        {
            return new RESTFulApiFactoryClient(httpClient);
        }
    }
}

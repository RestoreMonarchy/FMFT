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

        public async ValueTask<TResult> PostAsync<TContent, TResult>(string relativeUrl, TContent content)
        {
            return await apiClient.PostContentAsync<TContent, TResult>(relativeUrl, content);
        }

        public async ValueTask PostContentWithNoResponseAsync<TContent>(string relativeUrl, TContent content)
        {
            await apiClient.PostContentWithNoResponseAsync(relativeUrl, content);
        }

        private async ValueTask<TResult> PutAsync<TContent, TResult>(string relativeUrl, TContent content)
        {
            return await apiClient.PutContentAsync<TContent, TResult>(relativeUrl, content);
        }

        private async ValueTask<T> PutContentWithNoResponseAsync<T>(string relativeUrl, T content)
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

using FMFT.Web.Client.Models.API;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using System.Xml.Schema;

namespace FMFT.Web.Client.Brokers.APIs
{
    public partial class APIBroker : IAPIBroker
    {
        private readonly HttpClient httpClient;

        public APIBroker(IConfiguration configuration)
        {
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(configuration["APIUrl"])
            };

            httpClient.DefaultRequestHeaders.Add("Authorization", configuration["AccountToken"]);
        }

        private async ValueTask<APIResponse<T>> GetAsync<T>(string relativeUrl)
        {
            HttpResponseMessage response = await httpClient.GetAsync(relativeUrl);
            APIResponse<T> apiResponse = new(response);
            if (response.IsSuccessStatusCode)
            {
                await apiResponse.ReadObjectAsync();
            }
            
            return apiResponse;
        }

        public async ValueTask<APIResponse> GetAsync(string relativeUrl)
        {
            HttpResponseMessage response = await httpClient.GetAsync(relativeUrl);
            APIResponse apiResponse = new(response);
            
            return apiResponse;
        }

        public async ValueTask<APIResponse> PostAsync(string relativeUrl, object content)
        {            
            HttpResponseMessage response = await httpClient.PostAsJsonAsync(relativeUrl, content);
            APIResponse apiResponse = new(response);
            
            return apiResponse;
        }

        public async ValueTask<APIResponse<T>> PostAsync<T>(string relativeUrl, object content)
        {
            HttpResponseMessage response = await httpClient.PostAsJsonAsync(relativeUrl, content);
            APIResponse<T> apiResponse = new(response);
            if (response.IsSuccessStatusCode)
            {
                await apiResponse.ReadObjectAsync();
            }
            
            return apiResponse;
        }

        public async ValueTask<APIResponse> PutAsync(string relativeUrl, object content)
        {
            HttpResponseMessage response = await httpClient.PutAsJsonAsync(relativeUrl, content);
            APIResponse apiResponse = new(response);
            
            return apiResponse;
        }

        public async ValueTask<APIResponse<T>> PutAsync<T>(string relativeUrl, object content)
        {
            HttpResponseMessage response = await httpClient.PutAsJsonAsync(relativeUrl, content);
            APIResponse<T> apiResponse = new(response);
            if (response.IsSuccessStatusCode)
            {
                await apiResponse.ReadObjectAsync();
            }
            
            return apiResponse;
        }

        public async ValueTask DeleteAsync(string relativeUrl)
        {
            HttpResponseMessage response = await httpClient.DeleteAsync(relativeUrl);
            APIResponse apiResponse = new(response);
            
            return apiResponse;
        }
    }
}

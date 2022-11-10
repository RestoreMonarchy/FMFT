using FMFT.Web.Client.Models.API;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
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
        }

        public void SetAuthorizationToken(string authorizationToken)
        {
            httpClient.DefaultRequestHeaders.Remove("Authorization");
            httpClient.DefaultRequestHeaders.Add("Authorization", authorizationToken);
        }

        private async ValueTask<APIResponse<T>> GetAsync<T>(string relativeUrl)
        {
            HttpResponseMessage response = await httpClient.GetAsync(relativeUrl);
            APIResponse<T> apiResponse = new(response);

            await apiResponse.ReadContentAsync();
            
            return apiResponse;
        }

        private async ValueTask<APIResponse> GetAsync(string relativeUrl)
        {
            HttpResponseMessage response = await httpClient.GetAsync(relativeUrl);
            APIResponse apiResponse = new(response);

            await apiResponse.ReadContentAsync();

            return apiResponse;
        }

        private async ValueTask<APIResponse> PostAsync(string relativeUrl, object content)
        {
            HttpResponseMessage response;
            if (content == null)
            {
                response = await httpClient.PostAsync(relativeUrl, null);
            }
            else
            {
                response = await httpClient.PostAsJsonAsync(relativeUrl, content);
            }

            APIResponse apiResponse = new(response);

            await apiResponse.ReadContentAsync();

            return apiResponse;
        }

        private async ValueTask<APIResponse<T>> PostAsync<T>(string relativeUrl, object content)
        {
            HttpResponseMessage response;
            if (content == null)
            {
                response = await httpClient.PostAsync(relativeUrl, null);
            } else
            {
                response = await httpClient.PostAsJsonAsync(relativeUrl, content);
            }
             
            APIResponse<T> apiResponse = new(response);

            await apiResponse.ReadContentAsync();

            return apiResponse;
        }

        private async ValueTask<APIResponse> PostFileAsync(string relativeUrl, APIRequestFile apiRequestFile)
        {
            using MultipartFormDataContent content = new();

            StreamContent fileContent = new(apiRequestFile.FileStream);
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(apiRequestFile.ContentType);
            string fileContentName = "formfile";
            string fileName = apiRequestFile.FileName;

            content.Add(fileContent, fileContentName, fileName);

            HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(relativeUrl, content);

            return await APIResponse.CreateAsync(httpResponseMessage);
        }

        private async ValueTask<APIResponse> PutAsync(string relativeUrl, object content)
        {
            HttpResponseMessage response = await httpClient.PutAsJsonAsync(relativeUrl, content);
            APIResponse apiResponse = new(response);

            await apiResponse.ReadContentAsync();

            return apiResponse;
        }

        private async ValueTask<APIResponse<T>> PutAsync<T>(string relativeUrl, object content)
        {
            HttpResponseMessage response = await httpClient.PutAsJsonAsync(relativeUrl, content);
            APIResponse<T> apiResponse = new(response);

            await apiResponse.ReadContentAsync();

            return apiResponse;
        }

        private async ValueTask<APIResponse> DeleteAsync(string relativeUrl)
        {
            HttpResponseMessage response = await httpClient.DeleteAsync(relativeUrl);
            APIResponse apiResponse = new(response);

            await apiResponse.ReadContentAsync();

            return apiResponse;
        }
    }
}

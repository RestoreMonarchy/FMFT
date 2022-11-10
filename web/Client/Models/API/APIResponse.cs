using System.Net;
using System.Net.Http;
using System.Net.Http.Json;

namespace FMFT.Web.Client.Models.API
{
    public class APIResponse
    {
        public APIResponse(HttpResponseMessage httpResponseMessage)
        {
            HttpMessage = httpResponseMessage;
        }

        public static async ValueTask<APIResponse> CreateAsync(HttpResponseMessage httpResponseMessage)
        {
            APIResponse response = new(httpResponseMessage);

            if (!response.IsSuccessful)
            {
                response.Error = await httpResponseMessage.Content.ReadFromJsonAsync<APIError>();
            }

            return response;
        }

        public HttpStatusCode StatusCode => HttpMessage.StatusCode;
        public bool IsSuccessful => HttpMessage.IsSuccessStatusCode;
        public HttpResponseMessage HttpMessage { get; private set; }
        public APIError Error { get; private set; }

        public void ThrowIfError()
        {
            if (!IsSuccessful)
            {
                throw new Exception(Error.Title);
            }
        }

        public virtual async ValueTask ReadContentAsync()
        {
            if (!IsSuccessful)
            {
                await ReadErrorAsync();
            }
        }

        public async ValueTask<T> ReadFromJsonAsync<T>()
        {
            return await HttpMessage.Content.ReadFromJsonAsync<T>();
        }

        protected async ValueTask ReadErrorAsync()
        {
            Error = await HttpMessage.Content.ReadFromJsonAsync<APIError>();
        }
    }

    public class APIResponse<TObject> : APIResponse
    {
        public APIResponse(HttpResponseMessage httpResponseMessage) : base(httpResponseMessage)
        {

        }

        public override async ValueTask ReadContentAsync()
        {
            if (IsSuccessful)
            {
                await ReadObjectAsync();
            } else
            {
                await ReadErrorAsync();
            }
        }

        private async ValueTask ReadObjectAsync()
        {
            Object = await HttpMessage.Content.ReadFromJsonAsync<TObject>();
        }

        public TObject Object { get; set; }
    }
}

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

        public HttpStatusCode StatusCode => HttpMessage.StatusCode;
        public bool IsSuccessfull => HttpMessage.IsSuccessStatusCode;
        public HttpResponseMessage HttpMessage { get; private set; }
        public APIError Error { get; private set; }

        public virtual async ValueTask ReadContentAsync()
        {
            if (!IsSuccessfull)
            {
                await ReadErrorAsync();
            }
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
            if (IsSuccessfull)
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

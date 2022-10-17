using System.Net;
using System.Net.Http;
using System.Net.Http.Json;

namespace FMFT.Web.Client.Models.API
{
    public class APIResponse
    {
        public APIResponse(HttpResponseMessage responseMessage)
        {
            Message = responseMessage;
        }

        public HttpStatusCode StatusCode => Message.StatusCode;
        public HttpResponseMessage Message { get; set; }
    }

    public class APIResponse<T> : APIResponse
    {
        public APIResponse(HttpResponseMessage httpResponseMessage) : base(httpResponseMessage)
        {

        }

        public async ValueTask ReadObjectAsync()
        {
            Object = await Message.Content.ReadFromJsonAsync<T>();
        }

        public T Object { get; set; }
    }
}

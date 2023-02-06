using Microsoft.AspNetCore.Http;
using System.Text;

namespace FMFT.Extensions.Payments.Extensions
{
    public static class HttpContextExtensions
    {
        public static async ValueTask<string> ReadBodyToStringAsync(this HttpRequest request)
        {
            string bodyString;

            using (StreamReader reader = new(request.Body, Encoding.UTF8, true, 1024, true))
            {
                bodyString = await reader.ReadToEndAsync();
            }

            return bodyString;
        }
    }
}

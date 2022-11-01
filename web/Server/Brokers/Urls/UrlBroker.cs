using FMFT.Web.Server.Models.Options;
using Microsoft.Extensions.Options;

namespace FMFT.Web.Server.Brokers.Urls
{
    public class UrlBroker : IUrlBroker
    {
        private readonly ServicesOptions options;

        public UrlBroker(IOptions<ServicesOptions> options)
        {
            this.options = options.Value;
        }

        private string FormatUrl(string baseUrl, string relativeUrl, params object[] args)
        {
            string url = baseUrl.TrimEnd('/') + "/" + relativeUrl.TrimStart('/');

            return string.Format(url, args);
        }

        private string FormatClientUrl(string relativeUrl, params object[] args)
        {
            return FormatUrl(options.ClientBaseUrl, relativeUrl, args);
        }

        public string GetClientConfirmEmailUrl(int userId, Guid confirmSecret)
        {
            const string relativeUrl = "/users/{0}/confirmemail/{1}";

            return FormatClientUrl(relativeUrl, userId, confirmSecret);
        }

        public string GetClientResetPasswordEmailUrl(Guid secretKey)
        {
            const string relativeUrl = "/account/resetpassword/{0}";

            return FormatClientUrl(relativeUrl, secretKey);
        }
    }
}

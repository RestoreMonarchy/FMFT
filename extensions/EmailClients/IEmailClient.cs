using FMFT.Extensions.EmailClients.Models;

namespace FMFT.Extensions.EmailClients
{
    public interface IEmailClient
    {
        Task SendHtmlEmailMessageAsync(HtmlEmailMessage emailMessage);
    }
}

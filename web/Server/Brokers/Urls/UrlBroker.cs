using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

namespace FMFT.Web.Server.Brokers.Urls
{
    public class UrlBroker : IUrlBroker
    {
        private readonly LinkGenerator linkGenerator;

        public UrlBroker(LinkGenerator linkGenerator)
        {
            this.linkGenerator = linkGenerator;
        }

        public string Action(string action, string controller, object values)
        {
            return linkGenerator.GetPathByAction(action, controller, values);
        }        
    }
}

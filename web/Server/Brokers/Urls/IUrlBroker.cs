namespace FMFT.Web.Server.Brokers.Urls
{
    public interface IUrlBroker
    {
        string Action(string action, string controller, object values);
    }
}

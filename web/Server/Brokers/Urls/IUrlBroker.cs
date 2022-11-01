namespace FMFT.Web.Server.Brokers.Urls
{
    public interface IUrlBroker
    {
        string GetClientConfirmEmailUrl(int userId, Guid confirmSecret);
    }
}

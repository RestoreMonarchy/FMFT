namespace FMFT.Web.Client.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<bool> GetCookiesAlertFlagAsync();
        ValueTask SetCookiesAlertFlagAsync(bool value);
    }
}

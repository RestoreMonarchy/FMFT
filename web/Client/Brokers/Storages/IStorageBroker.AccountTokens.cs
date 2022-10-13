namespace FMFT.Web.Client.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<string> GetAccountTokenAsync();
        ValueTask SetAccountTokenAsync(string accountToken);
    }
}

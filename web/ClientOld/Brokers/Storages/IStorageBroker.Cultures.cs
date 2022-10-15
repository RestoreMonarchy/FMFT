using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Client.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<CultureId> GetCultureIdAsync();
        ValueTask SetCultureIdAsync(CultureId cultureId);
    }
}

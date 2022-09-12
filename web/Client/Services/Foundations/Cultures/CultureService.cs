using FMFT.Web.Client.Brokers.Localizations;
using FMFT.Web.Client.Brokers.Storages;
using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Client.Services.Foundations.Cultures
{
    public class CultureService : ICultureService
    {
        private readonly IStorageBroker storageBroker;

        public CultureService(IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;
        }

        public async ValueTask<CultureId> RetrieveCultureIdAsync()
        {
            return await storageBroker.GetCultureIdAsync();
        }

        public async ValueTask UpdateCultureIdAsync(CultureId cultureId)
        {
            await storageBroker.SetCultureIdAsync(cultureId);
        }
    }
}  

using FMFT.Web.Client.Brokers.Localizations;
using FMFT.Web.Client.Brokers.Storages;

namespace FMFT.Web.Client.Services.Foundations.Cultures
{
    public class CultureService : ICultureService
    {
        private readonly ILocalizationBroker localizationBroker;
        private readonly IStorageBroker storageBroker;

        public CultureService(ILocalizationBroker localizationBroker, IStorageBroker storageBroker)
        {
            this.localizationBroker = localizationBroker;
            this.storageBroker = storageBroker;
        }
    }
}

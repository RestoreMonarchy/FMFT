using FMFT.Web.Client.Resources;
using Microsoft.Extensions.Localization;
using System.Globalization;

namespace FMFT.Web.Client.Brokers.Localizations
{
    public class LocalizationBroker : ILocalizationBroker
    {
        private readonly IStringLocalizer<Localization> localizer;

        public LocalizationBroker(IStringLocalizer<Localization> localizer)
        {
            this.localizer = localizer;
        }

        public void SetGlobalCulture(CultureInfo cultureInfo)
        {
            CultureInfo.CurrentCulture = cultureInfo;
        }

        public LocalizedString this[string name] => localizer[name];
        public LocalizedString this[string name, params object[] arguments] => localizer[name, arguments];
    }
}

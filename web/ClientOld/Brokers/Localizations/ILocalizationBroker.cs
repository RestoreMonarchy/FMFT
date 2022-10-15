using System.Globalization;

namespace FMFT.Web.Client.Brokers.Localizations
{
    public interface ILocalizationBroker
    {
        void SetGlobalCulture(CultureInfo cultureInfo);
    }
}

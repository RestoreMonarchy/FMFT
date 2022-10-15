using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Client.Services.Foundations.Cultures
{
    public interface ICultureService
    {
        ValueTask<CultureId> RetrieveCultureIdAsync();
        ValueTask UpdateCultureIdAsync(CultureId cultureId);
    }
}
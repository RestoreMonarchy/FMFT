using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Client.Services.Processings.Cultures
{
    public interface ICultureProcessingService
    {
        ValueTask<CultureId> RetrieveCultureIdAsync();
        ValueTask UpdateCultureIdAsync(CultureId cultureId);
    }
}
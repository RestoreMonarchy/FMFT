using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Client.Services.Orchestrations.Cultures
{
    public interface ICultureOrchestrationService
    {
        ValueTask<CultureId> RetrieveCultureIdAsync();
        ValueTask UpdateCultureIdAsync(CultureId cultureId);
    }
}
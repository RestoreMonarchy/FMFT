using FMFT.Web.Client.Services.Processings.Cultures;
using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Client.Services.Orchestrations.Cultures
{
    public class CultureOrchestrationService : ICultureOrchestrationService
    {
        private readonly ICultureProcessingService cultureService;

        public CultureOrchestrationService(ICultureProcessingService cultureService)
        {
            this.cultureService = cultureService;
        }

        public async ValueTask<CultureId> RetrieveCultureIdAsync()
        {
            return await cultureService.RetrieveCultureIdAsync();
        }

        public async ValueTask UpdateCultureIdAsync(CultureId cultureId)
        {
            await cultureService.UpdateCultureIdAsync(cultureId);
        }
    }
}

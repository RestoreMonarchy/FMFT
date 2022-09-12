using FMFT.Web.Client.Services.Foundations.Cultures;
using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Client.Services.Processings.Cultures
{
    public class CultureProcessingService : ICultureProcessingService
    {
        private readonly ICultureService cultureService;

        public CultureProcessingService(ICultureService cultureService)
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

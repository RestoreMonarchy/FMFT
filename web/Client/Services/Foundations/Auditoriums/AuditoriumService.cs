using FMFT.Web.Client.Brokers.APIs;
using FMFT.Web.Server.Models.Auditoriums;
using FMFT.Web.Server.Models.Auditoriums.Exceptions;
using RESTFulSense.WebAssembly.Exceptions;

namespace FMFT.Web.Client.Services.Foundations.Auditoriums
{
    public class AuditoriumService : IAuditoriumService
    {
        private readonly IAPIBroker apiBroker;

        public AuditoriumService(IAPIBroker apiBroker)
        {
            this.apiBroker = apiBroker;
        }

        public async ValueTask<Auditorium> RetrieveAuditoriumByIdAsync(int auditoriumId)
        {
            try
            {
                Auditorium auditorium = await apiBroker.GetAuditoriumByIdAsync(auditoriumId);
                return auditorium;
            }
            catch (HttpResponseNotFoundException)
            {
                throw new AuditoriumNotFoundException();
            }
        }

        public async ValueTask<List<Auditorium>> RetrieveAllAuditoriumsAsync()
        {
            List<Auditorium> auditoriums = await apiBroker.GetAllAuditoriumsAsync();
            return auditoriums;
        }
    }
}

using FMFT.Web.Server.Brokers.Storages;
using FMFT.Web.Shared.Models.Auditoriums;
using FMFT.Web.Shared.Models.Auditoriums.Exceptions;

namespace FMFT.Web.Server.Services.Foundations.Auditoriums
{
    public class AuditoriumService : IAuditoriumService
    {
        private readonly IStorageBroker storageBroker;

        public AuditoriumService(IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;
        }

        public async ValueTask<Auditorium> RetrieveAuditoriumByIdAsync(int auditoriumId)
        {
            Auditorium auditorium = await storageBroker.SelectAuditoriumByIdAsync(auditoriumId);
            if (auditorium == null)
            {
                throw new AuditoriumNotFoundException();
            }

            return auditorium;
        }

        public async ValueTask<IEnumerable<Auditorium>> RetrieveAllAuditoriumsAsync()
        {
            IEnumerable<Auditorium> auditoriums = await storageBroker.SelectAllAuditoriumsAsync();

            return auditoriums;
        }
    }
}

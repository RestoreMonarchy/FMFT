using FMFT.Web.Server.Brokers.Loggings;
using FMFT.Web.Server.Brokers.Storages;
using FMFT.Web.Server.Models.Auditoriums;
using FMFT.Web.Server.Models.Auditoriums.Exceptions;

namespace FMFT.Web.Server.Services.Foundations.Auditoriums
{
    public partial class AuditoriumService : IAuditoriumService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public AuditoriumService(IStorageBroker storageBroker, ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<Auditorium> RetrieveAuditoriumByIdAsync(int auditoriumId)
        {
            Auditorium auditorium = await storageBroker.SelectAuditoriumByIdAsync(auditoriumId);
            if (auditorium == null)
            {
                throw new NotFoundAuditoriumException();
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

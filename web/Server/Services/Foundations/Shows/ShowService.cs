using FMFT.Web.Server.Brokers.Storages;
using FMFT.Web.Shared.Models.Shows;
using FMFT.Web.Shared.Models.Shows.Exceptions;

namespace FMFT.Web.Server.Services.Foundations.Shows
{
    public class ShowService : IShowService
    {
        private readonly IStorageBroker storageBroker;

        public ShowService(IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;
        }

        public async ValueTask<Show> RetrieveShowByIdAsync(int showId)
        {
            Show show = await storageBroker.SelectShowByIdAsync(showId);
            if (show == null) 
            {
                throw new ShowNotFoundException();
            }

            return show;
        }

        public async ValueTask<IEnumerable<Show>> RetrieveAllShowsAsync()
        {
            IEnumerable<Show> shows = await storageBroker.SelectAllShowsAsync();
            return shows;
        }
    }
}

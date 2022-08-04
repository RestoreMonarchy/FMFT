using FMFT.Web.Client.Brokers.APIs;
using FMFT.Web.Server.Models.Shows;
using FMFT.Web.Server.Models.Shows.Exceptions;
using RESTFulSense.WebAssembly.Exceptions;

namespace FMFT.Web.Client.Services.Foundations.Shows
{
    public class ShowService : IShowService
    {
        private readonly IAPIBroker apiBroker;

        public ShowService(IAPIBroker apiBroker)
        {
            this.apiBroker = apiBroker;
        }

        public async ValueTask<Show> RetrieveShowByIdAsync(int showId)
        {
            try
            {
                Show show = await apiBroker.GetShowByIdAsync(showId);
                return show;
            } catch (HttpResponseNotFoundException)
            {
                throw new ShowNotFoundException();
            }
        }

        public async ValueTask<List<Show>> RetrieveAllShowsAsync()
        {
            List<Show> shows = await apiBroker.GetAllShowsAsync();
            return shows;
        }
    }
}

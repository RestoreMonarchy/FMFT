using FMFT.Web.Client.Brokers.APIs;
using FMFT.Web.Client.Models.Shows;
using FMFT.Web.Client.Models.Shows.Exceptions;
using FMFT.Web.Client.Models.Shows.Params;
using FMFT.Web.Client.Models.Shows.Requests;
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

        public async ValueTask<Show> AddShowAsync(AddShowParams @params)
        {
            PostShowRequest request = new()
            {
                PublicId = @params.PublicId,
                Name = @params.Name,
                Description = @params.Description,
                StartDateTime = @params.StartDateTime,
                EndDateTime = @params.EndDateTime,
                AuditoriumId = @params.AuditoriumId
            };

            Show show = await apiBroker.PostShowAsync(request);
            return show;
        }

        public async ValueTask<Show> UpdateShowAsync(UpdateShowParams @params)
        {
            PutShowRequest request = new()
            {
                Id = @params.Id,
                PublicId = @params.PublicId,
                Name = @params.Name,
                Description = @params.Description,
                StartDateTime = @params.StartDateTime,
                EndDateTime = @params.EndDateTime,
                AuditoriumId = @params.AuditoriumId
            };

            Show show = await apiBroker.PutShowAsync(request);
            return show;
        }
    }
}

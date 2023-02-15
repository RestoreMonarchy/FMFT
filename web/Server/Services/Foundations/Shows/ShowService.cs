using FMFT.Web.Server.Brokers.Loggings;
using FMFT.Web.Server.Brokers.Storages;
using FMFT.Web.Server.Brokers.Validations;
using FMFT.Web.Server.Models.Database;
using FMFT.Web.Server.Models.Shows;
using FMFT.Web.Server.Models.Shows.Exceptions;
using FMFT.Web.Server.Models.Shows.Params;

namespace FMFT.Web.Server.Services.Foundations.Shows
{
    public partial class ShowService : IShowService
    {
        private readonly IStorageBroker storageBroker;
        private readonly IValidationBroker validationBroker;
        private readonly ILoggingBroker loggingBroker;

        public ShowService(IStorageBroker storageBroker, IValidationBroker validationBroker, ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.validationBroker = validationBroker;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<Show> RetrieveShowByIdAsync(int showId)
        {
            Show show = await storageBroker.SelectShowByIdAsync(showId);
            if (show == null)
            {
                throw new NotFoundShowException();
            }

            return show;
        }

        public async ValueTask<Show> RetrievePublicShowByIdAsync(int showId)
        {
            Show show = await storageBroker.SelectPublicShowByIdAsync(showId);
            if (show == null)
            {
                throw new NotFoundShowException();
            }

            return show;
        }

        public async ValueTask<IEnumerable<Show>> RetrieveAllShowsAsync()
        {
            IEnumerable<Show> shows = await storageBroker.SelectAllShowsAsync();
            
            return shows;
        }

        public async ValueTask<IEnumerable<Show>> RetrievePublicShowsAsync()
        {
            IEnumerable<Show> shows = await storageBroker.SelectPublicShowsAsync();

            return shows;
        }

        public async ValueTask<Show> AddShowAsync(AddShowParams @params)
        {
            ValidateAddShowParams(@params);

            StoredProcedureResult<Show> result = await storageBroker.ExecuteAddShowAsync(@params);

            if (result.ReturnValue == 1)
            {
                throw new AuditoriumNotExistsShowException();
            }

            return result.Result;
        }

        public async ValueTask<Show> ModifyShowAsync(UpdateShowParams @params)
        {
            ValidateUpdateShowParams(@params);

            StoredProcedureResult<Show> result = await storageBroker.ExecuteUpdateShowAsync(@params);

            if (result.ReturnValue == 1)
            {
                throw new AuditoriumNotExistsShowException();
            }

            if (result.ReturnValue == 2)
            {
                throw new NotFoundShowException();
            }

            return result.Result;
        }

        public async ValueTask<Show> ModifyShowSellingDetailsAsync(UpdateShowSellingDetailsParams @params)
        {
            ValidateUpdateShowSellingDetailsParams(@params);

            StoredProcedureResult<Show> result = await storageBroker.ExecuteUpdateShowSellingDetailsAsync(@params);

            if (result.ReturnValue == 1)
            {
                throw new NotFoundShowException();
            }

            return result.Result;
        }
    }
}

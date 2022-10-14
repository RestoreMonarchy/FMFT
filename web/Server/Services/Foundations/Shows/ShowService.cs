using FMFT.Extensions.TheStandard;
using FMFT.Web.Server.Brokers.Loggings;
using FMFT.Web.Server.Brokers.Storages;
using FMFT.Web.Server.Brokers.Validations;
using FMFT.Web.Server.Models.Database;
using FMFT.Web.Server.Models.Shows;
using FMFT.Web.Server.Models.Shows.Exceptions;
using FMFT.Web.Server.Models.Shows.Params;

namespace FMFT.Web.Server.Services.Foundations.Shows
{
    public partial class ShowService : TheStandardService, IShowService
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

        public ValueTask<Show> RetrieveShowByIdAsync(int showId)
            => TryCatch(async () =>
            {
                Show show = await storageBroker.SelectShowByIdAsync(showId);
                if (show == null)
                {
                    throw new NotFoundShowException();
                }

                return show;
            });

        public ValueTask<IEnumerable<Show>> RetrieveAllShowsAsync()
            => TryCatch(async () =>
            {
                IEnumerable<Show> shows = await storageBroker.SelectAllShowsAsync();
                return shows;
            });

        public ValueTask<Show> AddShowAsync(AddShowParams @params)
            => TryCatch(async () =>
            {
                ValidateAddShowParams(@params);

                StoredProcedureResult<Show> result = await storageBroker.ExecuteAddShowAsync(@params);

                if (result.ReturnValue == 1)
                {
                    throw new AuditoriumNotExistsShowException();
                }

                return result.Result;
            });

        public ValueTask<Show> ModifyShowAsync(UpdateShowParams @params)
            => TryCatch(async () =>
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
            });
    }
}

using FMFT.Web.Server.Brokers.Storages;
using FMFT.Web.Server.Brokers.Validations;
using FMFT.Web.Server.Models.Database;
using FMFT.Web.Server.Models.Shows;
using FMFT.Web.Server.Models.Shows.Exceptions;
using FMFT.Web.Server.Models.Shows.Params;

namespace FMFT.Web.Server.Services.Foundations.Shows
{
    public class ShowService : IShowService
    {
        private readonly IStorageBroker storageBroker;
        private readonly IValidationBroker validationBroker;

        public ShowService(IStorageBroker storageBroker, IValidationBroker validationBroker)
        {
            this.storageBroker = storageBroker;
            this.validationBroker = validationBroker;
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

        public async ValueTask<Show> AddShowAsync(AddShowParams @params)
        {
            AddShowValidationException validationException = new();
            if (validationBroker.IsStringInvalid(@params.PublicId, true, 255))
            {
                validationException.UpsertDataList("PublicId", "PublicId is required");
            }
            if (validationBroker.IsStringInvalid(@params.Name, true, 255))
            {
                validationException.UpsertDataList("Name", "Name is required");
            }
            if (validationBroker.IsStringInvalid(@params.Description, false, 4000))
            {
                validationException.UpsertDataList("Description", "Description must not exceed 4000 characters");
            }
            if (validationBroker.IsStartDateTimeInvalid(@params.StartDateTime))
            {
                validationException.UpsertDataList("StartDateTime", "StartDateTime must not be from the past");
            }
            if (validationBroker.IsDateTimeInOneYearRangeInvalid(@params.EndDateTime))
            {
                validationException.UpsertDataList("EndDateTime", "EndDateTime must not be later than one year in the future");
            }
            if (validationBroker.IsDateTimeRangeInvalid(@params.StartDateTime, @params.EndDateTime))
            {
                validationException.UpsertDataList("StartDateTime", "StartDateTime must be earlier than EndDateTime");
                validationException.UpsertDataList("EndDateTime", "EndDateTime must be later than StartDateTime");
            }

            validationException.ThrowIfContainsErrors();

            StoredProcedureResult<Show> result = await storageBroker.ExecuteAddShowAsync(@params);

            if (result.ReturnValue == 1)
            {
                throw new ShowAuditoriumNotExistsException();
            }

            return result.Result;
        }

        public async ValueTask<Show> ModifyShowAsync(UpdateShowParams @params)
        {
            UpdateShowValidationException validationException = new();
            if (validationBroker.IsStringInvalid(@params.PublicId, true, 255))
            {
                validationException.UpsertDataList("PublicId", "PublicId is required and must not excceed 255 characters");
            }
            if (validationBroker.IsStringInvalid(@params.Name, true, 255))
            {
                validationException.UpsertDataList("Name", "Name is required and must not exceed 255 characters");
            }
            if (validationBroker.IsStringInvalid(@params.Description, false, 4000))
            {
                validationException.UpsertDataList("Description", "Description must not exceed 4000 characters");
            }
            if (validationBroker.IsStartDateTimeInvalid(@params.StartDateTime))
            {
                validationException.UpsertDataList("StartDateTime", "StartDateTime must not be from the past");
            }
            if (validationBroker.IsDateTimeInOneYearRangeInvalid(@params.EndDateTime))
            {
                validationException.UpsertDataList("EndDateTime", "EndDateTime must not be later than one year in the future");
            }
            if (validationBroker.IsDateTimeRangeInvalid(@params.StartDateTime, @params.EndDateTime))
            {
                validationException.UpsertDataList("StartDateTime", "StartDateTime must be earlier than EndDateTime");
                validationException.UpsertDataList("EndDateTime", "EndDateTime must be later than StartDateTime");
            }

            validationException.ThrowIfContainsErrors();

            StoredProcedureResult<Show> result = await storageBroker.ExecuteUpdateShowAsync(@params);

            if (result.ReturnValue == 1)
            {
                throw new ShowAuditoriumNotExistsException();
            }

            if (result.ReturnValue == 2)
            {
                throw new ShowNotFoundException();
            }

            return result.Result;
        }
    }
}

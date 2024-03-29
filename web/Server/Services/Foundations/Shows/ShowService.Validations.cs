﻿using FMFT.Web.Server.Models.Shows.Exceptions;
using FMFT.Web.Server.Models.Shows.Params;

namespace FMFT.Web.Server.Services.Foundations.Shows
{
    public partial class ShowService
    {
        private void ValidateUpdateShowSellingDetailsParams(UpdateShowSellingDetailsParams @params)
        {
            UpdateShowSellingDetailsValidationException validationException = new();

            validationException.ThrowIfContainsErrors();
        }

        private void ValidateUpdateShowStatusParams(UpdateShowStatusParams @params)
        {
            UpdateShowStatusValidationException validationException = new();

            validationException.ThrowIfContainsErrors();
        }

        private void ValidateUpdateShowTimeParams(UpdateShowTimeParams @params)
        {
            UpdateShowTimeValidationException validationException = new();

            DateTime startDateTime = DateTime.SpecifyKind(@params.StartDateTime, DateTimeKind.Local);
            DateTime endDateTime = DateTime.SpecifyKind(@params.EndDateTime, DateTimeKind.Local);

            if (validationBroker.IsStartDateTimeInvalid(startDateTime))
            {
                validationException.UpsertDataList("StartDateTime", "StartDateTime must not be from the past");
            }
            if (validationBroker.IsDateTimeInOneYearRangeInvalid(endDateTime))
            {
                validationException.UpsertDataList("EndDateTime", "EndDateTime must not be later than one year in the future");
            }
            if (validationBroker.IsDateTimeRangeInvalid(startDateTime, endDateTime))
            {
                validationException.UpsertDataList("StartDateTime", "StartDateTime must be earlier than EndDateTime");
                validationException.UpsertDataList("EndDateTime", "EndDateTime must be later than StartDateTime");
            }

            validationException.ThrowIfContainsErrors();
        }

        private void ValidateUpdateShowParams(UpdateShowParams @params)
        {
            UpdateShowValidationException validationException = new();

            if (validationBroker.IsStringInvalid(@params.Name, true, 255))
            {
                validationException.UpsertDataList("Name", "Name is required and must not exceed 255 characters");
            }
            if (validationBroker.IsStringInvalid(@params.Description, false, 4000))
            {
                validationException.UpsertDataList("Description", "Description must not exceed 4000 characters");
            }

            validationException.ThrowIfContainsErrors();
        }

        private void ValidateAddShowParams(AddShowParams @params)
        {
            AddShowValidationException validationException = new();

            if (validationBroker.IsStringInvalid(@params.Name, true, 255))
            {
                validationException.UpsertDataList("Name", "Name is required");
            }
            if (validationBroker.IsStringInvalid(@params.Description, false, 4000))
            {
                validationException.UpsertDataList("Description", "Description must not exceed 4000 characters");
            }

            validationException.ThrowIfContainsErrors();
        }
    }
}

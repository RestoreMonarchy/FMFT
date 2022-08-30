namespace FMFT.Web.Server.Brokers.Validations
{
    public partial interface IValidationBroker
    {
        bool IsDateTimeInOneYearRangeInvalid(DateTimeOffset dateTime);
        bool IsStartDateTimeInvalid(DateTimeOffset startDateTime);
        bool IsDateTimeRangeInvalid(DateTimeOffset dateTimeFrom, DateTimeOffset dateTimeTo);
    }
}

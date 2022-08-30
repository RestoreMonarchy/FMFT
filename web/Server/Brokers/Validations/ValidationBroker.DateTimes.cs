namespace FMFT.Web.Server.Brokers.Validations
{
    public partial class ValidationBroker
    {
        public bool IsDateTimeInOneYearRangeInvalid(DateTimeOffset dateTime)
        {
            return !IsDateTimeInOneYearRangeValid(dateTime);
        }

        private bool IsDateTimeInOneYearRangeValid(DateTimeOffset dateTime)
        {
            return dateTime < DateTimeOffset.Now.AddYears(1);
        }

        public bool IsStartDateTimeInvalid(DateTimeOffset startDateTime)
        {
            return !IsStartDateTimeValid(startDateTime);
        }

        private bool IsStartDateTimeValid(DateTimeOffset startDateTime)
        {
            return startDateTime > DateTimeOffset.Now;
        }

        public bool IsDateTimeRangeInvalid(DateTimeOffset startDateTime, DateTimeOffset endDateTime)
        {
            return !IsDateTimeRangeValid(startDateTime, endDateTime);
        }

        private bool IsDateTimeRangeValid(DateTimeOffset startDateTime, DateTimeOffset endDateTime)
        {
            return startDateTime < endDateTime;
        }
    }
}

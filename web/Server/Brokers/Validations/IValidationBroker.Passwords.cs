namespace FMFT.Web.Server.Brokers.Validations
{
    public partial interface IValidationBroker
    {
        bool IsPasswordInvalid(string password);
    }
}

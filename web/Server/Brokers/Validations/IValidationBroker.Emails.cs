namespace FMFT.Web.Server.Brokers.Validations
{
    public partial interface IValidationBroker
    {
        bool IsEmailInvalid(string email);
    }
}

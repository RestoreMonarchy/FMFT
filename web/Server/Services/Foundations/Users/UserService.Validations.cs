using FMFT.Web.Server.Models.Users.Exceptions;
using FMFT.Web.Server.Models.Users.Params;

namespace FMFT.Web.Server.Services.Foundations.Users
{
    public partial class UserService
    {
        private void ValidateRegisterUserWithLoginParams(RegisterUserWithLoginParams @params)
        {
            RegisterUserWithLoginValidationException validationException = new();
            if (validationBroker.IsStringNullOrEmpty(@params.Email))
            {
                validationException.UpsertDataList("Email", "Email is required");
            }
            if (validationBroker.IsEmailInvalid(@params.Email))
            {
                validationException.UpsertDataList("Email", "Email is invalid");
            }
            if (validationBroker.IsStringNullOrEmpty(@params.FirstName))
            {
                validationException.UpsertDataList("FirstName", "First name is required");
            }
            if (validationBroker.IsStringInvalid(@params.FirstName, 255, 2))
            {
                validationException.UpsertDataList("FirstName", "First name must be at least 2 characters long");
            }
            if (validationBroker.IsStringNullOrEmpty(@params.LastName))
            {
                validationException.UpsertDataList("LastName", "Last name is required");
            }
            if (validationBroker.IsStringInvalid(@params.LastName, 255, 2))
            {
                validationException.UpsertDataList("LastName", "Last name must be at least 2 characters long");
            }
            if (validationBroker.IsStringInvalid(@params.ProviderName, true, 255, 0))
            {
                validationException.UpsertDataList("LoginProvider", "Invalid");
            }
            if (validationBroker.IsStringInvalid(@params.ProviderKey, true, 255, 0))
            {
                validationException.UpsertDataList("ProviderKey", "Invalid");
            }

            validationException.ThrowIfContainsErrors();
        }

        private void ValidateRegisterUserWithPasswordParams(RegisterUserWithPasswordProcessingParams @params)
        {
            RegisterUserWithPasswordValidationException validationException = new();
            if (validationBroker.IsStringNullOrEmpty(@params.Email))
            {
                validationException.UpsertDataList("Email", "Email is required");
            }
            if (validationBroker.IsEmailInvalid(@params.Email))
            {
                validationException.UpsertDataList("Email", "Email is invalid");
            }
            if (validationBroker.IsStringNullOrEmpty(@params.FirstName))
            {
                validationException.UpsertDataList("FirstName", "First name is required");
            }
            if (validationBroker.IsStringInvalid(@params.FirstName, 255, 2))
            {
                validationException.UpsertDataList("FirstName", "First name must be at least 2 characters long");
            }
            if (validationBroker.IsStringNullOrEmpty(@params.LastName))
            {
                validationException.UpsertDataList("LastName", "Last name is required");
            }
            if (validationBroker.IsStringInvalid(@params.LastName, 255, 2))
            {
                validationException.UpsertDataList("LastName", "Last name must be at least 2 characters long");
            }
            if (validationBroker.IsPasswordInvalid(@params.PasswordText))
            {
                validationException.UpsertDataList("PasswordText", "Password must be at least 8 characters long");
            }

            validationException.ThrowIfContainsErrors();
        }
    }
}

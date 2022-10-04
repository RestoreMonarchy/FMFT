using FMFT.Web.Server.Models.Users.Arguments;
using FMFT.Web.Server.Models.Users.Exceptions;
using FMFT.Web.Server.Models.Users.Params;

namespace FMFT.Web.Server.Services.Foundations.Users
{
    public partial class UserService
    {
        private void ValidateRegisterUserWithLoginParams(RegisterUserWithLoginParams @params)
        {
            RegisterUserWithLoginValidationException validationException = new();
            if (validationBroker.IsEmailInvalid(@params.Email))
            {
                validationException.UpsertDataList("Email", "Email is required and must be valid");
            }
            if (validationBroker.IsStringInvalid(@params.FirstName, true, 255, 3))
            {
                validationException.UpsertDataList("FirstName", "First name is required and must be at least 3 characters long");
            }
            if (validationBroker.IsStringInvalid(@params.LastName, true, 255, 3))
            {
                validationException.UpsertDataList("LastName", "Last name is required and must be at least 3 characters long");
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

        private void ValidateRegisterUserWithPasswordArgs(RegisterUserWithPasswordArguments args)
        {
            RegisterUserWithPasswordValidationException validationException = new();
            if (validationBroker.IsEmailInvalid(args.Email))
            {
                validationException.UpsertDataList("Email", "Email is required and must be valid");
            }
            if (validationBroker.IsStringInvalid(args.FirstName, true, 255, 3))
            {
                validationException.UpsertDataList("FirstName", "First name is required and must be at least 3 characters long");
            }
            if (validationBroker.IsStringInvalid(args.LastName, true, 255, 3))
            {
                validationException.UpsertDataList("LastName", "Last name is required and must be at least 3 characters long");
            }
            if (validationBroker.IsPasswordInvalid(args.PasswordText))
            {
                validationException.UpsertDataList("PasswordText", "Password must be at least 8 characters long");
            }

            validationException.ThrowIfContainsErrors();
        }
    }
}

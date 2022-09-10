using FMFT.Web.Server.Brokers.Authentications;
using FMFT.Web.Server.Brokers.Encryptions;
using FMFT.Web.Server.Brokers.Storages;
using FMFT.Web.Server.Brokers.Validations;
using FMFT.Web.Server.Models.Database;
using FMFT.Web.Server.Models.Users;
using FMFT.Web.Server.Models.Users.Arguments;
using FMFT.Web.Server.Models.Users.Exceptions;
using FMFT.Web.Server.Models.Users.Params;
using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Server.Services.Foundations.Users
{
    public class UserService : IUserService
    {
        private readonly IStorageBroker storageBroker;
        private readonly IValidationBroker validationBroker;
        private readonly IEncryptionBroker encryptionBroker;

        public UserService(IStorageBroker storageBroker,
            IValidationBroker validationBroker,
            IEncryptionBroker encryptionBroker)
        {
            this.storageBroker = storageBroker;
            this.validationBroker = validationBroker;
            this.encryptionBroker = encryptionBroker;
        }

        public async ValueTask<IEnumerable<User>> RetrieveAllUsersAsync()
        {
            return await storageBroker.SelectAllUsersAsync();
        }

        public async ValueTask<User> RetrieveUserByIdAsync(int userId)
        {
            User user = await storageBroker.SelectUserByIdAsync(userId);
            if (user == null)
            {
                throw new UserNotFoundException();
            }

            return user;
        }

        public async ValueTask<User> RetrieveUserByEmailAsync(string email)
        {
            User user = await storageBroker.SelectUserByEmailAsync(email);
            if (user == null)
            {
                throw new UserNotFoundException();
            }

            return user;
        }

        public async ValueTask<User> RetrieveUserByLoginAsync(string providerName, string providerKey)
        {
            User user = await storageBroker.SelectUserByLoginAsync(providerName, providerKey);
            if (user == null)
            {
                throw new UserNotFoundException();
            }

            return user;
        }

        public async ValueTask<User> RegisterUserWithPasswordAsync(RegisterUserWithPasswordArguments args)
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

            RegisterUserWithPasswordParams @params = new()
            {
                Email = args.Email,
                FirstName = args.FirstName,
                LastName = args.LastName,
                Role = UserRole.Guest,
                PasswordHash = encryptionBroker.HashPassword(args.PasswordText)
            };

            StoredProcedureResult<User> result = await storageBroker.RegisterUserWithPasswordAsync(@params);
            if (result.ReturnValue == 1)
            {
                throw new UserEmailAlreadyExistsException();
            }

            return result.Result;
        }

        public async ValueTask<User> RegisterUserWithLoginAsync(RegisterUserWithLoginParams @params)
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

            StoredProcedureResult<User> result = await storageBroker.RegisterUserWithLoginAsync(@params);
            if (result.ReturnValue == 1)
            {
                throw new UserEmailAlreadyExistsException();
            }
            if (result.ReturnValue == 2)
            {
                throw new UserLoginAlreadyExistsException();
            }

            return result.Result;
        }

        public async ValueTask UpdateUserRoleAsync(UpdateUserRoleParams @params)
        {
            StoredProcedureResult result = await storageBroker.UpdateUserRoleAsync(@params);

            if (result.ReturnValue == 1)
            {
                throw new UserNotFoundException();
            }

            if (result.ReturnValue == 2)
            {
                throw new UserRoleAlreadyExistsException();
            }
        }
    }
}

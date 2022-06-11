using FMFT.Web.Server.Brokers.Storages;
using FMFT.Web.Server.Brokers.Validations;
using FMFT.Web.Server.Models.Database;
using FMFT.Web.Shared.Models.Users;
using FMFT.Web.Shared.Models.Users.Exceptions;
using FMFT.Web.Shared.Models.Users.Params;

namespace FMFT.Web.Server.Services.Foundations.Users
{
    public class UserService : IUserService
    {
        private readonly IStorageBroker storageBroker;
        private readonly IValidationBroker validationBroker;

        public UserService(IStorageBroker storageBroker, IValidationBroker validationBroker)
        {
            this.storageBroker = storageBroker;
            this.validationBroker = validationBroker;
        }

        public async ValueTask<IEnumerable<User>> RetrieveAllUsersAsync()
        {
            return await storageBroker.SelectAllUsersAsync();
        }

        public async ValueTask<User> RetrieveUserByIdAsync(int userId)
        {
            return await storageBroker.SelectUserByIdAsync(userId);
        }

        public async ValueTask<User> RegisterUserWithPasswordAsync(RegisterUserWithPasswordParams @params)
        {
            if (validationBroker.IsEmailInvalid(@params.Email))
            {
                throw new UserEmailInvalidException();
            }
            if (validationBroker.IsStringInvalid(@params.FirstName, true, 255, 3))
            {
                throw new UserFirstNameInvalidException();
            }
            if (validationBroker.IsStringInvalid(@params.LastName, true, 255, 3))
            {
                throw new UserLastNameInvalidException();
            }

            StoredProcedureResult<User> result = await storageBroker.RegisterUserWithPasswordAsync(@params);
            if (result.ReturnValue == 1)
            {
                throw new UserEmailAlreadyExistsException();
            }

            return result.Result;
        }

        public async ValueTask<User> RegisterUserWithLoginAsync(RegisterUserWithLoginParams @params)
        {
            if (validationBroker.IsEmailInvalid(@params.Email))
            {
                throw new UserEmailInvalidException();
            }
            if (validationBroker.IsStringInvalid(@params.FirstName, true, 255, 3))
            {
                throw new UserFirstNameInvalidException();
            }
            if (validationBroker.IsStringInvalid(@params.LastName, true, 255, 3))
            {
                throw new UserLastNameInvalidException();
            }
            if (validationBroker.IsStringInvalid(@params.LoginProvider, true, 255, 0))
            {
                throw new UserLoginLoginProviderInvalidException();
            }
            if (validationBroker.IsStringInvalid(@params.ProviderKey, true, 255, 0))
            {
                throw new UserLoginProviderKeyInvalidException();
            }

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
    }
}

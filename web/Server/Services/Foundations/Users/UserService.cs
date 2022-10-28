using FMFT.Web.Server.Brokers.Encryptions;
using FMFT.Web.Server.Brokers.Loggings;
using FMFT.Web.Server.Brokers.Storages;
using FMFT.Web.Server.Brokers.Validations;
using FMFT.Web.Server.Models.Database;
using FMFT.Web.Server.Models.Users;
using FMFT.Web.Server.Models.Users.Exceptions;
using FMFT.Web.Server.Models.Users.Params;
using FMFT.Web.Server.Models.Users.Requests;
using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Server.Services.Foundations.Users
{
    public partial class UserService : IUserService
    {
        private readonly IStorageBroker storageBroker;
        private readonly IValidationBroker validationBroker;
        private readonly IEncryptionBroker encryptionBroker;
        private readonly ILoggingBroker loggingBroker;

        public UserService(IStorageBroker storageBroker,
            IValidationBroker validationBroker,
            IEncryptionBroker encryptionBroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.validationBroker = validationBroker;
            this.encryptionBroker = encryptionBroker;
            this.loggingBroker = loggingBroker;
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
                throw new NotFoundUserException();
            }

            return user;
        }

        public async ValueTask<User> RetrieveUserByEmailAsync(string email)
        {
            User user = await storageBroker.SelectUserByEmailAsync(email);
            if (user == null)
            {
                throw new NotFoundUserException();
            }

            return user;
        }

        public async ValueTask<User> RetrieveUserByLoginAsync(string providerName, string providerKey)
        {
            User user = await storageBroker.SelectUserByLoginAsync(providerName, providerKey);
            if (user == null)
            {
                throw new NotFoundUserException();
            }

            return user;
        }

        public async ValueTask<User> RetrieveUserByEmailAndPasswordAsync(string email, string passwordText)
        {
            User user = await RetrieveUserByEmailAsync(email);

            if (!encryptionBroker.VerifyPassword(passwordText, user.PasswordHash))
            {
                throw new NotMatchPasswordUserException();
            }

            return user;
        }

        public async ValueTask<User> RegisterUserWithPasswordAsync(RegisterUserWithPasswordRequest request)
        {
            ValidateRegisterUserWithPasswordParams(request);

            RegisterUserWithPasswordParams @params = new()
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Role = UserRole.Guest,
                PasswordHash = encryptionBroker.HashPassword(request.PasswordText)
            };

            StoredProcedureResult<User> result = await storageBroker.RegisterUserWithPasswordAsync(@params);
            if (result.ReturnValue == 1)
            {
                throw new AlreadyExistsEmailUserException();
            }

            return result.Result;
        }


        public async ValueTask<User> RegisterUserWithLoginAsync(RegisterUserWithLoginParams @params)
        {
            ValidateRegisterUserWithLoginParams(@params);

            StoredProcedureResult<User> result = await storageBroker.RegisterUserWithLoginAsync(@params);
            if (result.ReturnValue == 1)
            {
                throw new AlreadyExistsEmailUserException();
            }
            if (result.ReturnValue == 2)
            {
                throw new AlreadyExistsUserExternalLoginException();
            }

            return result.Result;
        }

        public async ValueTask UpdateUserRoleAsync(UpdateUserRoleParams @params)
        {
            StoredProcedureResult result = await storageBroker.UpdateUserRoleAsync(@params);

            if (result.ReturnValue == 1)
            {
                throw new NotFoundUserException();
            }

            if (result.ReturnValue == 2)
            {
                throw new AlreadyExistsUserRoleException();
            }
        }

        public async ValueTask UpdateUserCultureAsync(UpdateUserCultureParams @params)
        {
            StoredProcedureResult result = await storageBroker.UpdateUserCultureAsync(@params);

            if (result.ReturnValue == 1)
            {
                throw new NotFoundUserException();
            }

            if (result.ReturnValue == 2)
            {
                throw new AlreadyExistsUserCultureException();
            }
        }
    }
}

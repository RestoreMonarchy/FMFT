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

            if (string.IsNullOrEmpty(user.PasswordHash))
            {
                throw new NotMatchPasswordUserException();
            }

            if (!encryptionBroker.VerifyPassword(passwordText, user.PasswordHash))
            {
                throw new NotMatchPasswordUserException();
            }

            return user;
        }

        public async ValueTask<IEnumerable<UserLogin>> RetrieveUserLoginsByUserIdAsync(int userId)
        {
            return await storageBroker.SelectUserLoginsByUserIdAsync(userId);
        }

        public async ValueTask<User> RegisterUserWithPasswordAsync(RegisterUserWithPasswordRequest request)
        {
            ValidateRegisterUserWithPasswordParams(request);

            RegisterUserWithPasswordParams @params = new()
            {
                Email = request.Email.Trim(),
                FirstName = request.FirstName?.Trim() ?? null,
                LastName = request.LastName?.Trim() ?? null,
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

        public async ValueTask UpdateUserPasswordAsync(UpdateUserPasswordRequest request)
        {
            UpdateUserPasswordParams @params = new()
            {
                UserId = request.UserId,
                PasswordHash = encryptionBroker.HashPassword(request.PasswordText)
            };

            StoredProcedureResult result = await storageBroker.UpdateUserPasswordAsync(@params);

            if (result.ReturnValue == 1)
            {
                throw new NotPasswordUserException();
            }
        }

        public async ValueTask ChangeUserPasswordAsync(ChangeUserPasswordRequest request)
        {
            ValidateChangeUserPasswordRequest(request);

            User user = await RetrieveUserByIdAsync(request.UserId);

            if (!encryptionBroker.VerifyPassword(request.CurrentPasswordText, user.PasswordHash))
            {
                throw new NotMatchPasswordUserException();
            }

            UpdateUserPasswordRequest updateUserPasswordRequest = new()
            {
                UserId = user.Id,
                PasswordText = request.PasswordText
            };

            await UpdateUserPasswordAsync(updateUserPasswordRequest);
        }

        public async ValueTask ConfirmEmailAsync(int userId, Guid confirmSecret)
        {
            StoredProcedureResult result = await storageBroker.ConfirmEmailAsync(userId, confirmSecret);

            if (result.ReturnValue == 1)
            {
                throw new NotMatchConfirmEmailSecretUserException();
            }

            if (result.ReturnValue == 2)
            {
                throw new AlreadyConfirmedEmailUserException();
            }
        }

        public async ValueTask UpdateConfirmEmailSendDateAsync(int userId)
        {
            await storageBroker.UpdateUserConfirmEmailSendDateAsync(userId);
        }
    }
}

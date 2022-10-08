using FMFT.Extensions.TheStandard;
using FMFT.Web.Server.Brokers.Encryptions;
using FMFT.Web.Server.Brokers.Loggings;
using FMFT.Web.Server.Brokers.Storages;
using FMFT.Web.Server.Brokers.Validations;
using FMFT.Web.Server.Models.Database;
using FMFT.Web.Server.Models.Users;
using FMFT.Web.Server.Models.Users.Exceptions;
using FMFT.Web.Server.Models.Users.Params;
using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Server.Services.Foundations.Users
{
    public partial class UserService : TheStandardService, IUserService
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

        public ValueTask<IEnumerable<User>> RetrieveAllUsersAsync()
            => TryCatch(async () => 
            {
                return await storageBroker.SelectAllUsersAsync();
            });

        public ValueTask<User> RetrieveUserByIdAsync(int userId)
            => TryCatch(async () =>
            {
                User user = await storageBroker.SelectUserByIdAsync(userId);
                if (user == null)
                {
                    throw new NotFoundUserException();
                }

                return user;
            });

        public ValueTask<User> RetrieveUserByEmailAsync(string email)
            => TryCatch(async () => 
            {
                User user = await storageBroker.SelectUserByEmailAsync(email);
                if (user == null)
                {
                    throw new NotFoundUserException();
                }

                return user;
            });

        public ValueTask<User> RetrieveUserByLoginAsync(string providerName, string providerKey)
            => TryCatch(async () => 
            {
                User user = await storageBroker.SelectUserByLoginAsync(providerName, providerKey);
                if (user == null)
                {
                    throw new NotFoundUserException();
                }

                return user;
            });

        public ValueTask<User> RegisterUserWithPasswordAsync(RegisterUserWithPasswordProcessingParams args)
            => TryCatch(async () =>
            {
                ValidateRegisterUserWithPasswordArgs(args);

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
                    throw new AlreadyExistsEmailUserException();
                }

                return result.Result;
            });


        public ValueTask<User> RegisterUserWithLoginAsync(RegisterUserWithLoginParams @params)
            => TryCatch(async () =>
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
            });

        public ValueTask UpdateUserRoleAsync(UpdateUserRoleParams @params)
         => TryCatch(async () =>
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
         });

        public ValueTask UpdateUserCultureAsync(UpdateUserCultureParams @params)
            => TryCatch(async () =>
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
            });
    }
}

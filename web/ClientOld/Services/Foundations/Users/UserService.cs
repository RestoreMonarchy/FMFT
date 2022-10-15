using FMFT.Web.Client.Brokers.APIs;
using FMFT.Web.Client.Models.Users;
using FMFT.Web.Client.Models.Users.Exceptions;
using FMFT.Web.Client.Models.Users.Requests;
using RESTFulSense.WebAssembly.Exceptions;

namespace FMFT.Web.Client.Services.Foundations.Users
{
    public class UserService : IUserService
    {
        private readonly IAPIBroker apiBroker;

        public UserService(IAPIBroker apiBroker)
        {
            this.apiBroker = apiBroker;
        }

        public async ValueTask<List<User>> RetrieveAllUsersAsync()
        {
            return await apiBroker.GetAllUsersAsync();
        }

        public async ValueTask<User> RetrieveUserByIdAsync(int userId)
        {
            try
            {
                return await apiBroker.GetUserByIdAsync(userId);
            } catch (HttpResponseNotFoundException)
            {
                throw new UserNotFoundException();
            }            
        }

        public async ValueTask UpdateUserRoleAsync(UpdateUserRoleRequest request)
        {
            try
            {
                await apiBroker.UpdateUserRoleAsync(request);
            } catch (HttpResponseNotFoundException)
            {
                throw new UserNotFoundException();
            } catch (HttpResponseConflictException)
            {
                throw new UserRoleAlreadyExistException();
            }
        }

        public async ValueTask UpdateUserCultureAsync(UpdateUserCultureRequest request)
        {
            try
            {
                await apiBroker.UpdateUserCultureAsync(request);
            } catch (HttpResponseNotFoundException)
            {
                throw new UserNotFoundException();
            } catch (HttpResponseConflictException)
            {
                throw new UserCultureAlreadyExistException();
            }
        }
    }
}
